using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MonitorSensors.Data;
using MonitorSensors.Entities;
using MonitorSensors.Middlewares;
using MonitorSensors.Services;
using MonitorSensors.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();

var getConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(getConnectionString));

var jwtSecret = builder.Configuration["JWTKey:Secret"];
builder.Services.AddScoped<TokenValidationParameters>(x =>
{
    return new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ValidateLifetime = true
    };
});

builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<VerifyTokenMiddleware>();

// app.UseAuthentication();
// app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Sensor}/{action=Index}/{id?}");


app.Run();