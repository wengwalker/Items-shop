using Microsoft.AspNetCore.Builder;

namespace ItemsShop.Common.Api.Abstractions;

public interface IModuleMiddlewareConfigurator
{
    IApplicationBuilder Configure(IApplicationBuilder app);
}
