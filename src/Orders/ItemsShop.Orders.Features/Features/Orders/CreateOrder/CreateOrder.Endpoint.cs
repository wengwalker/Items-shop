using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
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
            .Produces<OrderResponse>();
    }

    private static async Task<IResult> Handle(
        [FromServices] ICreateOrderHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(cancellationToken);

        return response.IsSuccess
            ? Results.Created(OrdersRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
