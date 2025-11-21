using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace ItemsShop.Common.Api.Extensions;

public static class MapHealthChecksExtensions
{
    public static WebApplication MapHealthChecksEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/api/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        })
        .DisableHttpMetrics();

        return app;
    }
}
