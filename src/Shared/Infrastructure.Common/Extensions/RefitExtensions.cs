using Api.Common.Options;
using Infrastructure.Common.ExternalApi.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace Infrastructure.Common.Extensions;

public static class RefitExtensions
{
    public static IServiceCollection AddCatalogRefitExtensions(this IServiceCollection services, ExternalApiOptions options)
    {
        services.AddRefitClient<ICatalogApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(options.CatalogBaseUrl))
            .AddTransientHttpErrorPolicy(p => p
                .WaitAndRetryAsync(options.RetryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
            .AddTransientHttpErrorPolicy(p => p
                .CircuitBreakerAsync(options.EventsCountToBreak, TimeSpan.FromSeconds(options.DurationOfBreak)));

        return services;
    }
}
