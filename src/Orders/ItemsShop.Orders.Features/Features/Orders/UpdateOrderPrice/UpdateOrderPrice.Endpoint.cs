using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

public sealed record UpdateOrderPriceRequest(
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
            .Produces<UpdateOrderPriceResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid orderId,
        [FromBody] UpdateOrderPriceRequest request,
        [FromServices] IValidator<UpdateOrderPriceRequest> validator,
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
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
