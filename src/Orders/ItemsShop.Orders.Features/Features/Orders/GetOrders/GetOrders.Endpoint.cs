using FluentValidation;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Common.Application.Enums;
using ItemsShop.Orders.Features.Shared.Consts;
using ItemsShop.Orders.Features.Shared.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

// TODO: split this record
public sealed record GetOrdersRequest(
    [FromQuery] QuerySortType? sortType,
    [FromQuery] OrderStatus? status,
    [FromQuery] decimal? biggerOrEqualPrice,
    [FromQuery] decimal? lessOrEqualPrice,
    [FromQuery] DateTime? createdBefore,
    [FromQuery] DateTime? createdAfter,
    [FromQuery] DateTime? updatedBefore,
    [FromQuery] DateTime? updatedAfter);

public class GetOrdersEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(OrdersRouteConsts.BaseRoute, Handle)
            .WithName("GetOrders")
            .WithTags(OrdersTagConsts.OrdersEndpointTags)
            .WithSummary("Returns list of orders")
            .WithDescription("Returns list of orders and accepts query params: ")
            .Produces<List<OrderResponse>>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetOrdersRequest request,
        [FromServices] IValidator<GetOrdersRequest> validator,
        [FromServices] IGetOrdersHandler handler,
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
