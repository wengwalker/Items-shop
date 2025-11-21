using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

internal interface IGetOrderItemHandler : IHandler
{
    Task<Result<OrderItemResponse>> HandleAsync(GetOrderItemRequest request, CancellationToken cancellationToken);
}

internal sealed class GetOrderItemHandler(
    OrderDbContext context,
    ILogger<GetOrderItemHandler> logger)
    : IGetOrderItemHandler
{
    public async Task<Result<OrderItemResponse>> HandleAsync(GetOrderItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching OrderItem with Id {ItemId} from Order with Id {OrderId}", request.itemId, request.orderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.orderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result<OrderItemResponse>.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        var orderItem = await context.OrderItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.itemId && x.OrderId == request.orderId, cancellationToken);

        if (orderItem == null)
        {
            logger.LogInformation("OrderItem with Id {ItemId} from Order with Id {OrderId} does not exists", request.itemId, request.orderId);

            return Result<OrderItemResponse>.Failure($"OrderItem with Id {request.itemId} from Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        logger.LogInformation("Fetched OrderItem with Id {ItemId} from Order with Id {OrderId}", request.itemId, request.orderId);

        return Result<OrderItemResponse>.Success(orderItem.MapToResponse());
    }
}
