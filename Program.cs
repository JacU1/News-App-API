using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using News_App_API.Context;
using News_App_API.Handlers;
using News_App_API.Services;
using News_App_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using News_App_API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<NewsAPIContext>();

builder.Services.AddScoped<JwtHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins(new string[] { "http://localhost:4200", "https://localhost:4200" })
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

  builder.Services.AddControllersWithViews();

  builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-Xsrf-Token";
            options.SuppressXFrameOptionsHeader = true;
        });

    builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => false;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

var jwtSettings = builder.Configuration.GetSection("JwtSettings");  
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

builder.Services.AddTransient<ITokenInterface, TokenService>();
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddIdentity<UserDto, IdentityUser>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseCors("EnableCORS");
app.Run();
