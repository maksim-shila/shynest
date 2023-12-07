using Shynest.Identity.Data;

namespace Shynest.Identity.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitializeDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var sp = scope.ServiceProvider;
            try
            {
                var context = sp.GetRequiredService<AuthDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = sp.GetRequiredService<ILogger>();
                logger.LogError(ex, "An exception occured while app initialization");
            }
        }
    }
}
