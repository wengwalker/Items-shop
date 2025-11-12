using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

public sealed record GetOrderItemsRequest([FromRoute] Guid orderId);

public class GetOrderItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(OrderItemsRouteConsts.BaseRoute, Handle)
            .WithName("GetOrderItemsByOrderId")
            .WithTags(OrderItemsTagConsts.OrderItemsEndpointTags)
            .WithSummary("Returns all items from order")
            .WithDescription("Returns all items from order by providing order id in route")
            .Produces<GetOrderItemsResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetOrderItemsRequest request,
        [FromServices] IValidator<GetOrderItemsRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand();

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
