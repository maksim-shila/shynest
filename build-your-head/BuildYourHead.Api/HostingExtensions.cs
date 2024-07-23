using BuildYourHead.Api.Extensions;

namespace BuildYourHead.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors();

        builder.Services.AddDbContext(configuration);
        builder.Services.AddPersistence();
        builder.Services.AddApplicationServices();
        builder.Services.AddMappers();
        builder.Services.AddRequestHandlers();

        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddConsole();
        }

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });

        app.UseRouting();
        app.UseExceptionHandlerMiddleware();

        var controllerActionBuilder = app.MapControllers();
        if (app.Environment.IsDevelopment())
        {
            controllerActionBuilder.AllowAnonymous();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.ApplyMigrations();
        return app;
    }
}