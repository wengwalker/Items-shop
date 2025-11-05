using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Orders.Features.Shared.Consts;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.Orders.CreateOrder;

public class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(OrdersRouteConsts.BaseRoute, Handle)
            .WithName("CreateOrder")
            .WithTags(OrdersTagConsts.OrdersEndpointTags)
            .WithSummary("Creates a new order")
            .WithDescription("Creates a new order")
            .Produces<CreateOrderResponse>();
    }

    private static async Task<IResult> Handle(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateOrderCommand(), cancellationToken);

        return response.IsSuccess
            ? Results.Created(OrdersRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
