using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

public sealed record UpdateCartItemQuantityRequest(
    [FromRoute] Guid cartId,
    [FromRoute] Guid itemId,
    [FromBody] int Quantity);

public class UpdateCartItemQuantityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(CartItemsRouteConsts.UpdateCartItemQuantity, Handle)
            .WithName("UpdateCartItemQuantityById")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Updates the quantity of the required product in cart")
            .WithDescription("Updates the quantity of the required product in cart by providing cart id and item id in route and new quantity in body")
            .Produces<CartItemResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] UpdateCartItemQuantityRequest request,
        [FromServices] IValidator<UpdateCartItemQuantityRequest> validator,
        [FromServices] IUpdateCartItemQuantityHandler handler,
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
