using ItemsShop.Common.Api.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ItemsShop.Common.Api.Extensions;

public static class MapEndpointExtensions
{
    public static IServiceCollection RegisterEndpointsFromAssemblyContaining(this IServiceCollection services, Type marker)
    {
        var assembly = marker.Assembly;

        var endpointTypes = assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && t is { IsClass: true, IsAbstract: false, IsInterface: false });

        var serviceDescriptors = endpointTypes
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
