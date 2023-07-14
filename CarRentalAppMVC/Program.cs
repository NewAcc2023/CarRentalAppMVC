using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Interfaces;
using CarRentalAppMVC.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("CarDB");
builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<ICarRepo, CarRepo>();
builder.Services.AddScoped<IBrandRepo, BrandRepo>();
builder.Services.AddScoped<ICarBodyTypeRepo, CarBodyTypeRepo>();
builder.Services.AddScoped<IDriveTypeRepo, DriveTypeRepo>();
builder.Services.AddScoped<IRentOrderRepo, RentOrderRepo>();
builder.Services.AddScoped<IEngineTypeRepo, EngineTypeRepo>();
builder.Services.AddScoped<IGearBoxRepo, GearBoxRepo>();
builder.Services.AddScoped<IStatusRepo, StatusRepo>();

builder.Services.AddScoped<UserRepo>();

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
