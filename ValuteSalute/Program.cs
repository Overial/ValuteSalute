// ValuteSaluteApp
// https://github.com/Overial
// 2022

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ValuteSalute.Service;
using ValuteSalute.Domain;
using ValuteSalute.Domain.Repositories.Abstract;
using ValuteSalute.Domain.Repositories.EntityFramework;

////// Initialize app //////

var builder = WebApplication.CreateBuilder(args);

// Set up app config
builder.Configuration.Bind("Project", new Config());

// Add needed functionality as services
builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();

// Connect db context
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

// Set up identity system
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Set up authentication cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "WeatherFeatherAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Set up authorization policy
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy =>
    {
        policy.RequireRole("admin");
    });
});

// Set up MVC
builder.Services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
});

////// Build app //////

var app = builder.Build();

////// Middleware //////

// Enable development mode
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable static files
app.UseStaticFiles();

// Enable routing system
app.UseRouting();

// Enable cookie policy
app.UseCookiePolicy();

// Enaple authentication
app.UseAuthentication();

// Enable authorization
app.UseAuthorization();

// Register endpoints
app.MapControllerRoute
(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Run app
app.Run();
