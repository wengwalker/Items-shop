using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

internal sealed record UpdateOrderItemQuantityBody(
    int Quantity);

public sealed record UpdateOrderItemQuantityRequest(
    Guid OrderId,
    Guid ItemId,
    int Quantity);

public class UpdateOrderItemQuantityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(OrderItemsRouteConsts.UpdateOrderItemQuantity, Handle)
            .WithName("UpdateOrderItemQuantityById")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Updates the quantity of the specified product in order item")
            .WithDescription("Updates the quantity of the specified product in order item by providing order id and item id in route and new quantity in body")
            .Produces<OrderItemResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromBody] UpdateOrderItemQuantityBody body,
        [FromServices] IValidator<UpdateOrderItemQuantityRequest> validator,
        [FromServices] IUpdateOrderItemQuantityHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateOrderItemQuantityRequest(orderId, itemId, body.Quantity);

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
