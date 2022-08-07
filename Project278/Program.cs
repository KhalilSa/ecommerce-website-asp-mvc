using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project278.Data;
using Project278.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LocalConnectionString"))
);
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/AccessDenied";

});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options => {
//        options.LoginPath = "/Acount/Login";
//        options.AccessDeniedPath = "/AccessDenied";
//        options.Events = new CookieAuthenticationEvents()
//        {
//            OnSignedIn = async context => {
//                var principle = context.Principal;
//                if (principle.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
//                {
//                    if (principle.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value == "admin")
//                    {
//                        var claimsIdentity = principle.Identity as ClaimsIdentity;
//                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "a"));
//                    }
//                }
//                await Task.CompletedTask;
//            }
//        };
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
