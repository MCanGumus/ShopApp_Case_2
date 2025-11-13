using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Features.Products.Handlers.Commands;
using ShopApp.Core.Interfaces;
using ShopApp.Infrastructure.Context;
using ShopApp.Infrastructure.Persistence;
using StackExchange.Redis;
using System;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
