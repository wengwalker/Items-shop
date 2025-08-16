using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;

namespace Catalog.Api.Extensions;

public static class ProblemDetailsExtensions
{
    public static IServiceCollection AddProblemDetailsExtended(this IServiceCollection services)
    {
        return services.AddProblemDetails(x =>
        {
            x.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;

                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });
    }
}
