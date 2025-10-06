using FluentValidation;
using ItemsShop.Catalog.Features.Features.Products.CreateProduct;
using ItemsShop.Catalog.Infrastructure;
using ItemsShop.Common.Api.Extensions;
using Mediator.Lite.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalog.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterEndpointsFromAssemblyContaining(typeof(CreateProductEndpoint));

        services.AddMediator(typeof(CreateProductHandler).Assembly);

        services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

        services.AddCatalogInfrastructure(configuration);

        return services;
    }
}
