using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

public sealed record GetCartItemRequest(
    Guid CartId,
    Guid ItemId);

public class GetCartItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CartItemsRouteConsts.GetCartItem, Handle)
            .WithName("GetCartItemById")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Returns one item from cart")
            .WithDescription("Returns one item from cart by providing cart id and item id in route")
            .Produces<CartItemResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid cartId,
        [FromRoute] Guid itemId,
        [FromServices] IValidator<GetCartItemRequest> validator,
        [FromServices] IGetCartItemHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new GetCartItemRequest(cartId, itemId);

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
