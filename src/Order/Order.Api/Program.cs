using Api.Common.Extensions;
using Api.Common.Middlewares;
using Api.Common.Options;
using FluentValidation;
using Infrastructure.Common.Extensions;
using Mediator.Lite.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Order.Application.UseCases.Orders.AddOrder;
using Order.Application.UseCases.Orders.DeleteOrder;
using Order.Infrastructure.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddExternalApiOptions();

builder.Services.AddSerilog();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Items shop - Order API",
        Description = "Simple web marketplace, implemented in ASP .NET Core Web API"
    });
});

builder.Services.AddDbContext<OrderDbContext>(x => x
    .UseNpgsql(builder.Configuration.GetConnectionString(nameof(OrderDbContext)))
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
    .EnableDetailedErrors(builder.Environment.IsDevelopment()));

builder.Services.AddCatalogRefitExtensions(builder.Services.GetOptions<ExternalApiOptions>());

builder.Services.AddValidatorsFromAssemblyContaining<DeleteOrderCommandValidator>();

builder.Services.AddMediator(typeof(AddOrderCommand).Assembly);

builder.Services.AddProblemDetailsExtended();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.MigrateDatabaseAsync<OrderDbContext>();

    app.UseSwagger();

    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseRouting();

app.MapControllers();

await app.RunAsync();

await Log.CloseAndFlushAsync();
