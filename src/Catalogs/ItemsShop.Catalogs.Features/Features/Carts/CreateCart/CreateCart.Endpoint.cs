using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Carts.CreateCart;

public class CreateCartEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CartsRouteConsts.BaseRoute, Handle)
            .WithName("CreateCart")
            .WithTags(CartsTagConsts.CartsEndpointTags)
            .WithSummary("Creates a new cart")
            .WithDescription("Creates a new cart")
            .Produces<CartResponse>();
    }

    private static async Task<IResult> Handle(
        [FromServices] ICreateCartHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(cancellationToken);

        return response.IsSuccess
            ? Results.Created(CartsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
