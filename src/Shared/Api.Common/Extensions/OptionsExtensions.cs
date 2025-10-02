using Api.Common.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api.Common.Extensions;

public static class OptionsExtensions
{
    public static IServiceCollection AddExternalApiOptions(this IServiceCollection services)
    {
        services.AddOptions<ExternalApiOptions>()
            .BindConfiguration(nameof(ExternalApiOptions), x => x.ErrorOnUnknownConfiguration = true)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static T GetOptions<T>(this IServiceCollection services) where T : BaseOptions
    {
        return services.BuildServiceProvider().GetRequiredService<IOptions<T>>().Value;
    }
}
