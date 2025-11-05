using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ItemsShop.Common.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoreInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string[] activityModuleNames)
    {
        services
            .AddHostOpenTelemetry(configuration, activityModuleNames)
            .AddHostHealthChecks(configuration);

        return services;
    }

    private static IServiceCollection AddHostOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration,
        string[] activityModuleNames)
    {
        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("ItemsShop"))
            .WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ItemsShop"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddNpgsqlInstrumentation();

                metrics
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ItemsShop"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
                    .AddSource(activityModuleNames);

                tracing
                    .AddOtlpExporter(exporter =>
                    {
                        exporter.Endpoint = new Uri(configuration["OTEL_EXPORTER_ENDPOINT"]!);
                        exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
                    });
            });

        return services;
    }

    private static IServiceCollection AddHostHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("Postgres");

        services
            .AddHealthChecks()
            .AddNpgSql(
                connectionString: postgresConnectionString!,
                name: "PostgreSQL",
                tags: ["db", "sql", "postgresql"]);

        return services;
    }
}
