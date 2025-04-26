using Eshop.Presentation.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Config loading (customized to mirror Startup behavior)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddServices(builder.Configuration, builder.Environment);

var app = builder.Build();

// Use all middlewares via extension method
app.UseMiddlewares(builder.Environment);

app.Run();
