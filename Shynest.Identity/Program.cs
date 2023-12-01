using Microsoft.Extensions.FileProviders;
using Shynest.Identity.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddCustomDbContext(configuration);
builder.Services.AddCustomIdentity();

builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.InitializeDb();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});
app.UseRouting();
app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
