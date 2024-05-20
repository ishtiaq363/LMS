using LMS.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LMS.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Logout";
    options.AccessDeniedPath = $"/Identity/Logout";
});
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender,EmailSender>();
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
app.MapRazorPages();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapAreaControllerRoute(
//        name: "admin",
//        areaName: "admin",
//        pattern: "{area=admin}/{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapAreaControllerRoute(
//        name: "student",
//        areaName: "student",
//        pattern: "{area=student}/{controller=Home}/{action=Index}/{id?}");

//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "admin",
        areaName: "admin",
        pattern: "admin/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapAreaControllerRoute(
        name: "student",
        areaName: "StudentSide",
        pattern: "student/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "admin",
//        pattern: "admin/{controller=Home}/{action=Index}/{id?}",
//        defaults: new { area = "Admin" }); // specify the area for admin controllers

//    endpoints.MapControllerRoute(
//        name: "student",
//        pattern: "student/{controller=Home}/{action=Index}/{id?}",
//        defaults: new { area = "StudentSide" }); // specify the area for student controllers

//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});
app.Run();
