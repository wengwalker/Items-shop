using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

public sealed record CreateCartItemRequest(
    [FromRoute] Guid cartId,
    [FromBody] int Quantity,
    [FromBody] Guid ProductId);

public class CreateCartItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CartItemsRouteConsts.BaseRoute, Handle)
            .WithName("CreateCartItemByCartId")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Creates a new item in cart")
            .WithDescription("Creates a new item in cart by providing cart id in route and new quantity and product id in body")
            .Produces<CartItemResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] CreateCartItemRequest request,
        [FromServices] IValidator<CreateCartItemRequest> validator,
        [FromServices] ICreateCartItemHandler handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Created(CartItemsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
