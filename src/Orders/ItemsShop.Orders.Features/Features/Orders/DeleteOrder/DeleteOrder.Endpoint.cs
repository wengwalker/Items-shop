using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.Orders.DeleteOrder;

public sealed record DeleteOrderRequest([FromRoute] Guid orderId);

public class DeleteOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(OrdersRouteConsts.DeleteOrder, Handle)
            .WithName("DeleteOrderById")
            .WithTags(OrdersTagConsts.OrdersEndpointTags)
            .WithSummary("Deletes a order")
            .WithDescription("Deletes a order by providing order id in route")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] DeleteOrderRequest request,
        [FromServices] IValidator<DeleteOrderRequest> validator,
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
            ? Results.NoContent()
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
