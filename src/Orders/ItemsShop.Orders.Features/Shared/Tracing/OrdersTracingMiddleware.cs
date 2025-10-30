using ItemsShop.Orders.Features.Shared.Routes;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ItemsShop.Orders.Features.Shared.Tracing;

public class OrdersTracingMiddleware(RequestDelegate next)
{
    private static readonly ActivitySource activitySource = new(OrdersTracingConsts.ActivityModuleName);

    public async Task InvokeAsync(HttpContext context)
    {
        if (!DetermineOrdersModulePaths(context))
        {
            await next(context);

            return;
        }

        var operationName = GetOperationName(context);
        using var activity = activitySource.StartActivity($"{OrdersTracingConsts.ModuleName}.{operationName}");

        activity?.SetTag("module", OrdersTracingConsts.ModuleName);
        activity?.SetTag("http.method", context.Request.Method);
        activity?.SetTag("http.path", context.Request.Path);
        activity?.SetTag("operation", operationName);

        try
        {
            await next(context);

            activity?.SetTag("http.status_code", context.Response.StatusCode);

            activity?.SetStatus(context.Response.StatusCode >= 400
                ? ActivityStatusCode.Error
                : ActivityStatusCode.Ok);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity?.SetTag("error.message", ex.Message);
            throw;
        }
    }

    private static bool DetermineOrdersModulePaths(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(OrdersRouteConsts.BaseRoute, StringComparison.Ordinal)
            || context.Request.Path.StartsWithSegments(OrderItemsRouteConsts.BaseRoute, StringComparison.Ordinal))
        {
            return true;
        }

        return false;
    }

    private static string GetOperationName(HttpContext context)
    {
        var method = context.Request.Method.ToUpper();

        var methodMapping = method switch
        {
            "POST" => "create",
            "GET" => "get",
            "PUT" => "update",
            "PATCH" => "update",
            "DELETE" => "delete",
            _ => method.ToLower()
        };

        return $"{OrdersTracingConsts.ModuleName}.{methodMapping}";
    }
}
