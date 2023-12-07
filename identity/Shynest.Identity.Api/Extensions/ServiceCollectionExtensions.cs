using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shynest.Identity.Data;

namespace Shynest.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ConnectionString");
            if (connectionString == null)
            {
                throw new ArgumentException("Connection string is not specified in configuration");
            }
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }

        public static void AddCustomIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Shynest.Identity.Cookie";
                config.LoginPath = "/auth/login";
                config.LogoutPath = "/auth/logout";
            });
        }
    }
}
