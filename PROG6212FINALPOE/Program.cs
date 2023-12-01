using DataHandeling;
using DataHandeling.Repository;
using DataHandeling.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

var solutionRootDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
while (!Directory.GetFiles(solutionRootDirectory, "*.sln").Any())
{
    solutionRootDirectory = Directory.GetParent(solutionRootDirectory).FullName;
}

builder.Services.AddRazorPages();
builder.Services.AddScoped<ModuleService>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<UserAuthentication>();

builder.Services.AddAuthentication("YourCookieAuthScheme")
    .AddCookie("YourCookieAuthScheme", options =>
    {
        options.LoginPath = "/Login/Index"; // Your login page path
        options.AccessDeniedPath = "/AccessDenied"; // Access denied path
        // Additional options
    });

// Calculate the connection string
var connectionString = $"Server=(localdb)\\mssqllocaldb;AttachDbFilename={Path.Combine(solutionRootDirectory, "DataHandeling\\App_Data\\MyDatabase.mdf")};Database=MyDatabase;Trusted_Connection=True;";

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(
        connectionString,
        builder => builder.MigrationsAssembly("DataHandeling")); // Specify the assembly name containing the migrations
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
