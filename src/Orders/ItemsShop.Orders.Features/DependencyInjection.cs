using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Tracing;
using ItemsShop.Orders.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Orders.Features;

public static class OrdersModuleExtensions
{
    public static string ActivityModuleName => OrdersTracingConsts.ActivityModuleName;

    public static IServiceCollection AddOrderModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddOrderModuleApi()
            .AddOrderInfrastructure(configuration);
    }

    public static IServiceCollection AddOrderModuleApi(this IServiceCollection services)
    {
        //services.RegisterEndpointsFromAssemblyContaining(typeof(CreateProductEndpoint));

        //services.AddMediator(typeof(CreateProductHandler).Assembly);

        //services.AddValidatorsFromAssembly(typeof(CreateProductRequestValidator).Assembly);

        return services;
    }
}

public class OrdersMiddlewareConfigurator : IModuleMiddlewareConfigurator
{
    public IApplicationBuilder Configure(IApplicationBuilder app)
    {
        return app.UseMiddleware<OrdersTracingMiddleware>();
    }
}
