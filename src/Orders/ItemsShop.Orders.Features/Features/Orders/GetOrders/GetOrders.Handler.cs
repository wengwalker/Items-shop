using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

internal interface IGetOrdersHandler : IHandler
{
    Task<Result<List<OrderResponse>>> HandleAsync(GetOrdersRequest request, CancellationToken cancellationToken);
}

internal sealed class GetOrdersHandler(
    OrderDbContext context,
    ILogger<GetOrdersHandler> logger)
    : IGetOrdersHandler
{
    public async Task<Result<List<OrderResponse>>> HandleAsync(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching orders");

        var query = context.Orders
            .AsQueryable()
            .AsNoTracking();

        if (request.status is not null)
        {
            logger.LogInformation("Fetching orders with specified status: {Status}", request.status);

            query = query.Where(x => x.Status == (byte)request.status);
        }

        if (request.biggerOrEqualPrice is not null)
        {
            logger.LogInformation("Fetching orders with a price greated than or equal to: {Price}", request.biggerOrEqualPrice);

            query = query.Where(x => x.TotalPrice >= request.biggerOrEqualPrice);
        }

        if (request.lessOrEqualPrice is not null)
        {
            logger.LogInformation("Fetching orders with a price less than or equal to: {Price}", request.lessOrEqualPrice);

            query = query.Where(x => x.TotalPrice <= request.lessOrEqualPrice);
        }

        // TODO: conditions for CreatedBefore/CreatedAfter/UpdatedBefore/UpdatedAfter

        if (request.sortType is not null)
        {
            logger.LogInformation("Fetching orders in specified order: {SortType}", request.sortType);

            query = request.sortType == QuerySortType.Ascending
                ? query.OrderBy(x => x.TotalPrice)
                : query.OrderByDescending(x => x.TotalPrice);
        }

        List<OrderResponse> orders = await query
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        logger.LogInformation("Fetched {Count} orders", orders.Count);

        return Result<List<OrderResponse>>.Success(orders);
    }
}
