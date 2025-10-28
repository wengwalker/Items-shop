using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

public sealed record CreateCartItemRequest(
    int Quantity,
    Guid ProductId);

public class CreateCartItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CartItemsRouteConsts.BaseRoute, Handle)
            .WithName("CreateCartItemByCartId")
            .WithTags("CartItems")
            .Produces<CreateCartItemResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid cartId,
        [FromBody] CreateCartItemRequest request,
        [FromServices] IValidator<CreateCartItemRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(cartId);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Created(CartItemsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
