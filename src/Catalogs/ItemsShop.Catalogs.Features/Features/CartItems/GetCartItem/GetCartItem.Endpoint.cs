using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

public sealed record GetCartItemRequest([FromRoute] Guid cartId, [FromRoute] Guid itemId);

public class GetCartItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CartItemsRouteConsts.GetCartItem, Handle)
            .WithName("GetCartItemById")
            .WithTags(CartItemsTagConsts.CartItemsEndpointTags)
            .WithSummary("Returns one item from cart")
            .WithDescription("Returns one item from cart by providing cart id and item id in route")
            .Produces<GetCartItemResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetCartItemRequest request,
        [FromServices] IValidator<GetCartItemRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand();

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
