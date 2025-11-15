using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopApp.Application.Features.Products.Handlers.Commands;
using ShopApp.Core.Interfaces;
using ShopApp.Core.Models;
using ShopApp.Infrastructure.Context;
using ShopApp.Infrastructure.Persistence;
using StackExchange.Redis;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // günlük log
    .CreateLogger();

builder.Host.UseSerilog();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly("ShopApp.Infrastructure")));

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse("localhost:6379", true);
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommandHandler).Assembly));

var jwtSettings = new JwtSettings
{
    Key = Environment.GetEnvironmentVariable("JWT__KEY")!,
    Issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!,
    Audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!
};

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerFeature =
            context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

        if (exceptionHandlerFeature?.Error != null)
        {
            var ex = exceptionHandlerFeature.Error;

            Log.Error(ex, "Unhandled exception occurred");

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message
            });
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
