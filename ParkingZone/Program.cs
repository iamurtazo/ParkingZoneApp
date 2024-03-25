using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ParkingZone.Areas.Identity.Data;
using ParkingZone.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ParkingZoneDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ParkingZoneDbContextConnection' not found.");

builder.Services.AddDbContext<ParkingZoneDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ParkingZoneUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<ParkingZoneDbContext>();


builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
