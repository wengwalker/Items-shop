using FluentValidation;
using ItemsShop.Catalogs.Features.Features.Products.CreateProduct;
using ItemsShop.Catalogs.Infrastructure;
using ItemsShop.Common.Api.Extensions;
using Mediator.Lite.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalogs.Features;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCatalogApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterEndpointsFromAssemblyContaining(typeof(CreateProductEndpoint));

        services.AddMediator(typeof(CreateProductHandler).Assembly);

        services.AddValidatorsFromAssembly(typeof(CreateProductRequestValidator).Assembly);

        services.AddCatalogInfrastructure(configuration);

        return services;
    }
}
