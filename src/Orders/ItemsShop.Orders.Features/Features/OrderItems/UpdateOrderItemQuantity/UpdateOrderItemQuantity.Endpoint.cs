using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

public sealed record UpdateOrderItemQuantityRequest(int Quantity);

public class UpdateOrderItemQuantityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(OrderItemsRouteConsts.UpdateOrderItemQuantity, Handle)
            .WithName("UpdateOrderItemQuantityById")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Updates the quantity of the specified product in order item")
            .WithDescription("Updates the quantity of the specified product in order item by providing order id and item id in route and new quantity in body")
            .Produces<UpdateOrderItemQuantityResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromBody] UpdateOrderItemQuantityRequest request,
        [FromServices] IValidator<UpdateOrderItemQuantityRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(orderId, itemId);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
