using Microsoft.AspNetCore.Authentication.Cookies;   // ??
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<SporKulubuContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SporKulubuConnection")));

// ??  Kimlik doðrulama + yetkilendirme
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
        opt.LogoutPath = "/Account/Logout";
        opt.AccessDeniedPath = "/Home/AccessDenied";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

// pipeline
if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   // ??
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
