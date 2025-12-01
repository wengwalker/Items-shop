using ItemsShop.Common.Api.ErrorHandling;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace ItemsShop.Common.Api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCoreApiInfrastructure(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ItemsShop API",
                    Version = "v1"
                });
            });

        services
            .AddProblemDetailsExtended()
            .AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static void AddCoreHostLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Logging.AddConsole();

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            options.ParseStateValues = true;

            options
                .SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddService(builder.Environment.ApplicationName))
                .AddOtlpExporter();
        });

        builder.Logging.Configure(options =>
        {
            options.ActivityTrackingOptions =
                ActivityTrackingOptions.SpanId |
                ActivityTrackingOptions.TraceId |
                ActivityTrackingOptions.ParentId |
                ActivityTrackingOptions.Baggage |
                ActivityTrackingOptions.Tags;
        });
    }

    public static void AddServiceProviderValidation(this WebApplicationBuilder builder)
    {
        builder.Host.UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        });
    }
}
