using ItemsShop.Catalogs.Features.Shared.Consts;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace ItemsShop.Catalogs.Features.Shared.Tracing;

public class CatalogsTracingMiddleware(RequestDelegate next)
{
    private static readonly ActivitySource activitySource = new(CatalogsTracingConsts.ActivityModuleName);

    public async Task InvokeAsync(HttpContext context)
    {
        if (!DetermineCatalogsModulePaths(context))
        {
            await next(context);

            return;
        }

        var operationName = GetOperationName(context);
        using var activity = activitySource.StartActivity($"{CatalogsTracingConsts.ModuleName}.{operationName}");

        activity?.SetTag("module", CatalogsTracingConsts.ModuleName);
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

    private static bool DetermineCatalogsModulePaths(HttpContext context)
    {
        return context.Request.Path.StartsWithSegments(ProductsRouteConsts.BaseRoute, StringComparison.Ordinal)
            || context.Request.Path.StartsWithSegments(CategoriesRouteConsts.BaseRoute, StringComparison.Ordinal)
            || context.Request.Path.StartsWithSegments(CartsRouteConsts.BaseRoute, StringComparison.Ordinal)
            || context.Request.Path.StartsWithSegments(CartItemsRouteConsts.BaseRoute, StringComparison.Ordinal);
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

        return $"{CatalogsTracingConsts.ModuleName}.{methodMapping}";
    }
}
