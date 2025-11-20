using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

public sealed record CreateOrderItemRequest(
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
            .Produces<CreateOrderItemResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromBody] CreateOrderItemRequest request,
        [FromServices] IValidator<CreateOrderItemRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(orderId);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Created(OrderItemsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
