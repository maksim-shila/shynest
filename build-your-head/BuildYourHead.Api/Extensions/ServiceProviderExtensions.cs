using BuildYourHead.Api.Controllers;
using BuildYourHead.Api.Options;
using BuildYourHead.Application.Mappers.Impl;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Application.Services;
using BuildYourHead.Application.Services.Impl;
using BuildYourHead.Infrastructure.ImageStorage;
using BuildYourHead.Infrastructure.ImageStorage.Db;
using BuildYourHead.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BuildYourHead.Api.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IRecipeService, RecipeService>();

            services.AddTransient<IImageStorage, DbImageStorage>();
            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<IProductMapper, ProductMapper>();
            services.AddTransient<IRecipeMapper, RecipeMapper>();
        }

        public static void AddRequestHandlers(this IServiceCollection services)
        {
            services.AddTransientAllChildrenOf<IRequestHandler>();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                throw new ArgumentException("Connection string is not specified in configuration");
            }
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion("8.0.32"));
            });
        }

        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImageStorageOptions>(configuration.GetSection("ImageStorage"));
        }

        private static void AddTransientAllChildrenOf<T>(this IServiceCollection services)
        {
            var parentType = typeof(T);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t != parentType && parentType.IsAssignableFrom(t))
                .ToList();
            foreach (var type in types)
            {
                services.AddTransient(type);
            }
        }
    }
}
