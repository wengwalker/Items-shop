using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.DeleteOrder;

internal interface IDeleteOrderHandler : IHandler
{
    Task<Result> HandleAsync(DeleteOrderRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteOrderHandler(
    OrderDbContext context,
    ILogger<DeleteOrderHandler> logger)
    : IDeleteOrderHandler
{
    public async Task<Result> HandleAsync(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting order with Id: {OrderId}", request.orderId);

        var order = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.orderId, cancellationToken);

        if (order == null)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted order with Id: {OrderId}", order.Id);

        return Result.Success();
    }
}
