using ItemsShop.Catalogs.Features;
using ItemsShop.Common.Api;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Common.Infrastructure;
using ItemsShop.Common.Infrastructure.Extensions;
using ItemsShop.Orders.Features;

var builder = WebApplication.CreateBuilder(args);

builder.AddCoreHostLogging();

builder.Services.AddCoreApiInfrastructure();

builder.Services.AddCoreInfrastructure(builder.Configuration,
[
    CatalogsModuleExtensions.ActivityModuleName,
    OrdersModuleExtensions.ActivityModuleName
]);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddOrderModule(builder.Configuration);

builder.AddServiceProviderValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    await scope.MigrateDatabasesAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseRouting();

app.UseModuleMiddlewares();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.MapHealthChecksEndpoints();

app.MapEndpoints();

await app.RunAsync();
