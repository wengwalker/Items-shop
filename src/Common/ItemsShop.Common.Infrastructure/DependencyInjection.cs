using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ItemsShop.Common.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, IConfiguration configuration, string[] activityModuleNames)
    {
        services
            .AddHostOpenTelemetry(activityModuleNames)
            .AddHostHealthChecks(configuration);

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

    private static IServiceCollection AddHostHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("Postgres");

        services
            .AddHealthChecks()
            .AddNpgSql(
                connectionString: postgresConnectionString!,
                name: "PostgreSQL",
                tags: ["db", "sql", "postgresql"]);

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }
}
