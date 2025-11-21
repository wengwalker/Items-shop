using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

public sealed record GetOrderItemsRequest(
    [FromRoute] Guid orderId);

public class GetOrderItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(OrderItemsRouteConsts.BaseRoute, Handle)
            .WithName("GetOrderItemsByOrderId")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Returns all items from order")
            .WithDescription("Returns all items from order by providing order id in route")
            .Produces<List<OrderItemResponse>>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetOrderItemsRequest request,
        [FromServices] IValidator<GetOrderItemsRequest> validator,
        [FromServices] IGetOrderItemsHandler handler,
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
