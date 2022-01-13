using AllShow;
using AllShow.Interface;
using AllShowService;
using AllShowService.Interface;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
//using NLog.Web;
using AllShow.Data;
using AllShow.Models.Identity;
using prjAllShow.Backend.Resources;
using prjAllShow.Backend.Seed;
using Microsoft.AspNetCore.Mvc;
//using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

//E:\\CSharp\\AllShowProject\\slnAllShow\\prjAllShow.Backend\\
string path = builder.Environment.ContentRootPath;

builder.Services.AddDbContext<AllShowDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AllShowDBContext")));
builder.Services.AddDbContext<IdentityDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDBContext")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWorks, UnitOfWork>();
builder.Services.AddScoped<IUnitOfWorksPlus, UnitOfWorkPlus>();
builder.Services.AddScoped<IEmployeeSettingService, EmployeeSettingService>();

//double minute = builder.Configuration.GetValue<double>("EXPIRY_DURATION_MINUTES");
//builder.Services.AddSingleton<ITokenService>(new TokenService(minute));
//builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
            options.User.AllowedUserNameCharacters = null;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
})
        .AddEntityFrameworkStores<IdentityDBContext>()
        //.AddDefaultUI()
        .AddDefaultTokenProviders();
/*
 services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.User.AllowedUserNameCharacters = "allowed characters here";
    options.User.RequireUniqueEmail = true/false;
});
 */

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,
        AdditionalUserClaimsPrincipalFactory>();

builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policyIsAdminRequirement =>
    {
        policyIsAdminRequirement.Requirements.Add(new IsAdminRequirement());
    });
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin", "SuperAdmin")/*.RequireClaim(JwtClaimTypes.Role, "admin")*/);
    options.AddPolicy("RequireFactoryRole", policy => policy.RequireRole("Factory")/*.RequireClaim(JwtClaimTypes.Role, "factory")*/);
});

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Default Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//});
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Default SignIn settings.
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc(options => {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    })
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResources));
    });

builder.Services.AddControllersWithViews();
//builder.Services.AddMvc();

double LoginExpireMinute = builder.Configuration.GetValue<double>("LoginExpireMinute");
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Account/Login";
    options.LogoutPath = $"/Account/Logout";
    options.AccessDeniedPath = $"/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(LoginExpireMinute);
    options.SlidingExpiration = false;
});
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//builder.Host.ConfigureLogging(logging=>
//{ 
//    logging.ClearProviders();
//    logging.SetMinimumLevel(LogLevel.Trace);
//}).UseNLog();

var app = builder.Build();

var supportedCultures = new[] { "zh-TW", "en-US" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
