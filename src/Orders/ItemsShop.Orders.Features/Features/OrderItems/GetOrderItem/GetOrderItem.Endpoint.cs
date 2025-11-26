using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

public sealed record GetOrderItemRequest(
    Guid OrderId,
    Guid ItemId);

public class GetOrderItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(OrderItemsRouteConsts.GetOrderItem, Handle)
            .WithName("GetOrderItemById")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Returns one item from order")
            .WithDescription("Returns one item from order by providing order id and item id in route")
            .Produces<OrderItemResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromRoute] Guid itemId,
        [FromServices] IValidator<GetOrderItemRequest> validator,
        [FromServices] IGetOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new GetOrderItemRequest(orderId, itemId);

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
