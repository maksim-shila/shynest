using BuildYourHead.Api.Controllers.RequestHandlers;
using BuildYourHead.Application.Mappers.Impl;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Application.Services;
using BuildYourHead.Application.Services.Impl;
using BuildYourHead.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Api.Extensions;

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
        services.AddTransient<IImageService, ImageService>();
    }

    public static void AddMappers(this IServiceCollection services)
    {
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