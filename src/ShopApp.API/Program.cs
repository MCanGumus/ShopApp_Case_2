using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Interfaces;
using ShopApp.Infrastructure.Context;
using ShopApp.Infrastructure.Persistence;
using System;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString,
    b => b.MigrationsAssembly("ShopApp.Infrastructure")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();
