using ItemsShop.Common.Api.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Common.Api.Extensions;

public static class MiddlewareRegistrationExtensions
{
    public static IApplicationBuilder UseModuleMiddlewares(this IApplicationBuilder app)
    {
        var configurators = app.ApplicationServices.GetServices<IModuleMiddlewareConfigurator>();

        foreach (var configurator in configurators)
        {
            configurator.Configure(app);
        }

        return app;
    }
}
