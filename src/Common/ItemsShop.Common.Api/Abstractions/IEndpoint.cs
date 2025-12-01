using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Common.Api.Abstractions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}
