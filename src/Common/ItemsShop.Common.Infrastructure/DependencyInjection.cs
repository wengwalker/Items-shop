using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ItemsShop.Common.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, string[] activityModuleNames)
    {
        services.AddHostOpenTelemetry(activityModuleNames);

        return services;
    }

    private static IServiceCollection AddHostOpenTelemetry(this IServiceCollection services, string[] activityModuleNames)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("ItemsShop"))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
                    .AddSource(activityModuleNames);

                tracing
                    .AddOtlpExporter();
            });

        return services;
    }
}
