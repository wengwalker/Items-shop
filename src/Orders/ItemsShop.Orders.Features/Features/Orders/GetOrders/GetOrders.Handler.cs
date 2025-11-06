using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

public sealed record GetOrdersQuery(
    QuerySortType? SortType,
    OrderStatus? Status,
    decimal? BiggerOrEqualPrice,
    decimal? LessOrEqualPrice,
    DateTime? CreatedBefore,
    DateTime? CreatedAfter,
    DateTime? UpdatedBefore,
    DateTime? UpdatedAfter) : IRequest<Result<GetOrdersResponse>>;

public sealed record GetOrdersResponse(ICollection<OrderResponse> Orders);

public sealed class GetOrdersHandler(
    OrderDbContext context,
    ILogger<GetOrdersHandler> logger) : IRequestHandler<GetOrdersQuery, Result<GetOrdersResponse>>
{
    public async Task<Result<GetOrdersResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching orders");

        var query = context.Orders
            .AsQueryable()
            .AsNoTracking();

        if (request.Status is not null)
        {
            logger.LogInformation("Fetching orders with specified status: {Status}", request.Status);

            query = query.Where(x => x.Status == (byte)request.Status);
        }

        if (request.BiggerOrEqualPrice is not null)
        {
            logger.LogInformation("Fetching orders with a price greated than or equal to: {Price}", request.BiggerOrEqualPrice);

            query = query.Where(x => x.TotalPrice >= request.BiggerOrEqualPrice);
        }

        if (request.LessOrEqualPrice is not null)
        {
            logger.LogInformation("Fetching orders with a price less than or equal to: {Price}", request.LessOrEqualPrice);

            query = query.Where(x => x.TotalPrice <= request.LessOrEqualPrice);
        }

        // TODO: conditions for CreatedBefore/CreatedAfter/UpdatedBefore/UpdatedAfter

        if (request.SortType is not null)
        {
            logger.LogInformation("Fetching orders in specified order: {SortType}", request.SortType);

            query = request.SortType == QuerySortType.Ascending
                ? query.OrderBy(x => x.TotalPrice)
                : query.OrderByDescending(x => x.TotalPrice);
        }

        List<OrderResponse> orders = await query
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        var response = orders.MapToResponse();

        logger.LogInformation("Fetched {Count} orders", response.Orders.Count);

        return Result<GetOrdersResponse>.Success(response);
    }
}
