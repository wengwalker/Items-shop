using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

internal interface IGetOrderItemsHandler : IHandler
{
    Task<Result<List<OrderItemResponse>>> HandleAsync(GetOrderItemsRequest request, CancellationToken cancellationToken);
}

internal sealed class GetOrderItemsHandler(
    OrderDbContext context,
    ILogger<GetOrderItemsHandler> logger)
    : IGetOrderItemsHandler
{
    public async Task<Result<List<OrderItemResponse>>> HandleAsync(GetOrderItemsRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching OrderItems from Order with Id {OrderId}", request.orderId);

        bool orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.orderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result<List<OrderItemResponse>>.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        List<OrderItemResponse> orderItems = await context.OrderItems
            .AsNoTracking()
            .Where(x => x.OrderId == request.orderId)
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        logger.LogInformation("Fetched OrderItems from Order with Id {OrderId}", request.orderId);

        return Result<List<OrderItemResponse>>.Success(orderItems);
    }
}
