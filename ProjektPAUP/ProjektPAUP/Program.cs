using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjektPAUP.Areas.Identity.Data;
using ProjektPAUP.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ProjektPAUPContextConnection") ?? throw new InvalidOperationException("Connection string 'ProjektPAUPContextConnection' not found.");

builder.Services.AddDbContext<ProjektPAUPContext>(options => options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<ProjektPAUPUser>(options =>
{

    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ProjektPAUPContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.IOTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.Name = "Kolacici";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
