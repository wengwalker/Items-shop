using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.DeleteOrderItem;

internal interface IDeleteOrderItemHandler : IHandler
{
    Task<Result> HandleAsync(DeleteOrderItemRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteOrderItemHandler(
    OrderDbContext context,
    ILogger<DeleteOrderItemHandler> logger)
    : IDeleteOrderItemHandler
{
    public async Task<Result> HandleAsync(DeleteOrderItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting item with Id {ItemId} from order with id {OrderId}", request.itemId, request.orderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.orderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        var orderItem = await context.OrderItems
            .FirstOrDefaultAsync(x => x.Id == request.itemId, cancellationToken);

        if (orderItem == null)
        {
            logger.LogInformation("Item with Id {ItemId} does not exists", request.itemId);

            return Result.Failure($"Item with Id {request.itemId} does not exists", ErrorType.NotFound);
        }

        context.OrderItems.Remove(orderItem);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted item with Id {ItemId} from order with Id: {OrderId}", request.itemId, request.orderId);

        return Result.Success();
    }
}
