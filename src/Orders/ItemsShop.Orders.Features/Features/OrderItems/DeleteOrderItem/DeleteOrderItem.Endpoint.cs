using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.DeleteOrderItem;

public sealed record DeleteOrderItemRequest(
    Guid OrderId,
    Guid ItemId);

public class DeleteOrderItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(OrderItemsRouteConsts.DeleteOrderItem, Handle)
            .WithName("DeleteOrderItemById")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Deletes an item from order")
            .WithDescription("Deletes an item from order by providing order id in ")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromServices] IValidator<DeleteOrderItemRequest> validator,
        [FromServices] IDeleteOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteOrderItemRequest(orderId, itemId);

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
