using ItemsShop.Common.Api.ErrorHandling;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace ItemsShop.Common.Api;

public static class DependencyInjection
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
        builder.Host.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration));
    }
}
