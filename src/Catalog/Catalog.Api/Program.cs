using Api.Common.Extensions;
using Api.Common.Middlewares;
using Catalog.Application.UseCases.Products.Commands.AddProduct;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Extensions;
using FluentValidation;
using Mediator.Lite.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Items shop API",
        Description = "Simple web marketplace, implemented in ASP .NET Core Web API"
    });
});

builder.Services.AddDbContext<CatalogDbContext>(x => x
    .UseNpgsql(builder.Configuration.GetConnectionString(nameof(CatalogDbContext)))
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
    .EnableDetailedErrors(builder.Environment.IsDevelopment()));

builder.Services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();

builder.Services.AddMediator(typeof(AddProductCommand).Assembly);

builder.Services.AddProblemDetailsExtended();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MigrateDatabase<CatalogDbContext>();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseRouting();

app.MapControllers();

await app.RunAsync();

Log.CloseAndFlush();
