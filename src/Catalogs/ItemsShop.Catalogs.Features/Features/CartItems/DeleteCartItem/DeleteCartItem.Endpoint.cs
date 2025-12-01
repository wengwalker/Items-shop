using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.DeleteCartItem;

public sealed record DeleteCartItemRequest(
    Guid CartId,
    Guid ItemId);

public class DeleteCartItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(CartItemsRouteConsts.DeleteCartItem, Handle)
            .WithName("DeleteCartItemById")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Deletes an item from cart")
            .WithDescription("Deletes an item from cart by providing cart id and item id in route")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid cartId,
        [FromRoute] Guid itemId,
        [FromServices] IValidator<DeleteCartItemRequest> validator,
        [FromServices] IDeleteCartItemHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteCartItemRequest(cartId, itemId);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.NoContent()
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
