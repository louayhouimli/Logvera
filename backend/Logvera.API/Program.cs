using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Logvera.API.Application.Apis;
using Logvera.API.Application.Auth;
using Logvera.API.Application.Logs;
using Logvera.API.Application.Analytics;
using Logvera.API.Application.Alerts;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<LogveraDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=logvera;Username=logvera;Password=logvera"));

builder.Services.AddControllers();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IAlertRuleService, AlertRuleService>();
builder.Services.AddScoped<IAlertService, AlertService>();


builder.Services.AddHostedService<AlertEvaluationService>();





builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .Select(x => x.Value!.Errors.First().ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(new
        {
            message = "Validation failed",
            errors
        });
    };
});



var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("accessToken"))
            {
                context.Token = context.Request.Cookies["accessToken"];
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("LogveraCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "https://logvera.com")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LogveraDbContext>();
    db.Database.CanConnect();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("LogveraCors");

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();

