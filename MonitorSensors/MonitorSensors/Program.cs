using Microsoft.EntityFrameworkCore;
using MonitorSensors.Data;
using MonitorSensors.Services;
using MonitorSensors.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISensorService, SensorService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sensor}/{action=Index}/{id?}");

app.Run();