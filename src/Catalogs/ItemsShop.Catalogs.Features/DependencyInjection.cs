using FluentValidation;
using ItemsShop.Catalogs.Features.InternalApi;
using ItemsShop.Catalogs.Features.Shared.Tracing;
using ItemsShop.Catalogs.Infrastructure;
using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Common.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalogs.Features;

public static class CatalogsModuleExtensions
{
    public static string ActivityModuleName => CatalogsTracingConsts.ActivityModuleName;

    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddCatalogModuleApi()
            .AddCatalogInfrastructure(configuration);
    }

    private static IServiceCollection AddCatalogModuleApi(this IServiceCollection services)
    {
        services.AddScoped<ICatalogModuleApi, CatalogModuleApi>();

        services.RegisterEndpointsFromAssemblyContaining(typeof(CatalogsModuleExtensions));

        services.RegisterHandlersFromAssemblyContaining(typeof(CatalogsModuleExtensions));

        services.AddValidatorsFromAssembly(typeof(CatalogsModuleExtensions).Assembly);

        return services;
    }
}

public class CatalogsMiddlewareConfigurator : IModuleMiddlewareConfigurator
{
    public IApplicationBuilder Configure(IApplicationBuilder app)
    {
        return app.UseMiddleware<CatalogsTracingMiddleware>();
    }
}
