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
        logger.LogInformation("Deleting item with Id {ItemId} from order with id {OrderId}", request.ItemId, request.OrderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result.Failure($"Order with Id {request.OrderId} does not exists", ErrorType.NotFound);
        }

        var orderItem = await context.OrderItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (orderItem == null)
        {
            logger.LogInformation("Item with Id {ItemId} does not exists", request.ItemId);

            return Result.Failure($"Item with Id {request.ItemId} does not exists", ErrorType.NotFound);
        }

        context.OrderItems.Remove(orderItem);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted item with Id {ItemId} from order with Id: {OrderId}", request.ItemId, request.OrderId);

        return Result.Success();
    }
}
