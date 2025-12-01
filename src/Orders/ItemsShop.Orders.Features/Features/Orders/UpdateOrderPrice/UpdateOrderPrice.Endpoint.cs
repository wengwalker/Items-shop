using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

internal sealed record UpdateOrderPriceBody(
    decimal Price);

public sealed record UpdateOrderPriceRequest(
    Guid OrderId,
    decimal Price);

public class UpdateOrderPriceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(OrdersRouteConsts.UpdateOrderPrice, Handle)
            .WithName("UpdateOrderPriceById")
            .WithTags(OrdersTagConsts.OrdersEndpointTags)
            .WithSummary("Updates an order total price")
            .WithDescription("Updates an order total price by providing order id in route and price in body")
            .Produces<OrderResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromBody] UpdateOrderPriceBody body,
        [FromServices] IValidator<UpdateOrderPriceRequest> validator,
        [FromServices] IUpdateOrderPriceHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateOrderPriceRequest(orderId, body.Price);

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
