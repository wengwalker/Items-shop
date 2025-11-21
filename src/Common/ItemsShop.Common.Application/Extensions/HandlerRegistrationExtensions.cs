using ItemsShop.Common.Domain.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ItemsShop.Common.Application.Extensions;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection RegisterHandlersFromAssemblyContaining(this IServiceCollection services, Type marker)
    {
        var assembly = marker.Assembly;

        RegisterCommandHandlers(services, assembly);

        return services;
    }

    private static void RegisterCommandHandlers(IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                && t.IsAssignableTo(typeof(IHandler)))
            .ToList();

        foreach (var implementationType in handlerTypes)
        {
            var interfaceType = implementationType.GetInterfaces()
                .FirstOrDefault(i => i != typeof(IHandler) && i.IsAssignableTo(typeof(IHandler)));

            if (interfaceType is not null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
        }
    }
}
