using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Shynest.Identity.Server;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
        var connectionString = builder.Configuration.GetConnectionString("Identity");

        void ConfigureDbContext(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion("8.0.32"),
                sqlOptions => { sqlOptions.MigrationsAssembly(migrationsAssembly); });
        }

        builder.Services.AddIdentityServer()
            .AddConfigurationStore(options => { options.ConfigureDbContext = ConfigureDbContext; })
            .AddOperationalStore(options => { options.ConfigureDbContext = ConfigureDbContext; })
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

        return app;
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        persistedGrantDbContext.Database.Migrate();

        var configurationDbContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configurationDbContext.Database.Migrate();

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