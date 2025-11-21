using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

public sealed record GetCartItemsRequest([FromRoute] Guid cartId);

public class GetCartItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CartItemsRouteConsts.BaseRoute, Handle)
            .WithName("GetCartItemsByCartId")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Returns all items from cart")
            .WithDescription("Returns all items from cart by providing cart id in route")
            .Produces<List<CartItemResponse>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetCartItemsRequest request,
        [FromServices] IValidator<GetCartItemsRequest> validator,
        [FromServices] IGetCartItemsHandler handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
