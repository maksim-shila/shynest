using System.Reflection;
using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shynest.Identity.Api.Data;
using Shynest.Identity.Api.Models;

namespace Shynest.Identity.Api;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddControllers();

        var mySqlVersion = new MySqlServerVersion("8.0.32");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Application");
                options.UseMySql(connectionString, mySqlVersion);
            }
        );

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer()
            .AddAspNetIdentity<ApplicationUser>()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = dbContextOptionsBuilder =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("Configuration");
                    dbContextOptionsBuilder.UseMySql(connectionString, mySqlVersion, optionsBuilder =>
                    {
                        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                    });
                };
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = dbContextOptionsBuilder =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("PersistedGrant");
                    dbContextOptionsBuilder.UseMySql(connectionString, mySqlVersion, optionsBuilder =>
                    {
                        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
                        optionsBuilder.MigrationsAssembly(migrationsAssembly);
                    });
                };
            })
            .AddTestUsers(TestUsers.Users);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var pathBase = app.Configuration.GetValue<string>("Application:PathBase");
        app.UsePathBase(pathBase);

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCookiePolicy();

        InitializeDatabase(app);

        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages().RequireAuthorization();
        app.MapControllers();

        return app;
    }

    private static void InitializeDatabase(IHost app)
    {
        using var serviceScope = app.Services.CreateScope();

        var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        persistedGrantDbContext.Database.Migrate();

        var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configurationDbContext.Database.Migrate();

        var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.Migrate();

        var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var alice = userMgr.FindByNameAsync("alice").Result;
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(alice, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                new Claim(JwtClaimTypes.GivenName, "Alice"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("alice created");
        }
        else
        {
            Log.Debug("alice already exists");
        }

        var bob = userMgr.FindByNameAsync("bob").Result;
        if (bob == null)
        {
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(bob, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Bob Smith"),
                new Claim(JwtClaimTypes.GivenName, "Bob"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                new Claim("location", "somewhere")
            }).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            Log.Debug("bob created");
        }
        else
        {
            Log.Debug("bob already exists");
        }

        if (!configurationDbContext.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                configurationDbContext.Clients.Add(client.ToEntity());
            }

            configurationDbContext.SaveChanges();
        }

        if (!configurationDbContext.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                configurationDbContext.IdentityResources.Add(resource.ToEntity());
            }

            configurationDbContext.SaveChanges();
        }

        if (!configurationDbContext.ApiScopes.Any())
        {
            foreach (var apiScope in Config.ApiScopes)
            {
                configurationDbContext.ApiScopes.Add(apiScope.ToEntity());
            }

            configurationDbContext.SaveChanges();
        }
    }
}