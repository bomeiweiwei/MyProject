using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NLog.Web;
using prjAllShow.Backend.Data;

var builder = WebApplication.CreateBuilder(args);

//E:\\CSharp\\AllShowProject\\slnAllShow\\prjAllShow.Backend\\
string path = builder.Environment.ContentRootPath;

builder.Services.AddDbContext<AllShowDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AllShowDBContext")));

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Host.ConfigureLogging(logging=>
{ 
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
}).UseNLog();

var app = builder.Build();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
    RequestPath = new PathString("/scripts")
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = new PathString("/vendor")
});

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
