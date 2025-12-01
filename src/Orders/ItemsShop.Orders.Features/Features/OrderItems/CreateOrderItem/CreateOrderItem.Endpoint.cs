using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

internal sealed record CreateOrderItemBody(
    long Quantity,
    Guid ProductId);

public sealed record CreateOrderItemRequest(
    Guid OrderId,
    long Quantity,
    Guid ProductId);

public class CreateOrderItemEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(OrderItemsRouteConsts.BaseRoute, Handle)
            .WithName("CreateOrderItemByOrderId")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Creates a new item in order")
            .WithDescription("Creates a new item in order by providing order id in route and quantity and product id in body")
            .Produces<OrderItemResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromBody] CreateOrderItemBody body,
        [FromServices] IValidator<CreateOrderItemRequest> validator,
        [FromServices] ICreateOrderItemHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new CreateOrderItemRequest(orderId, body.Quantity, body.ProductId);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Created(OrderItemsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
