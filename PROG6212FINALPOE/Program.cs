using DataHandeling;
using DataHandeling.Repository;
using DataHandeling.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication("YourCookieAuthScheme")
    .AddCookie("YourCookieAuthScheme", options =>
    {
        options.LoginPath = "/Login/Index"; // Your login page path
        options.AccessDeniedPath = "/AccessDenied"; // Access denied path
        // Additional options
    });
// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MyDbConnection' not found.")));

builder.Services.AddScoped<ModuleService>();
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<UserAuthentication>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
