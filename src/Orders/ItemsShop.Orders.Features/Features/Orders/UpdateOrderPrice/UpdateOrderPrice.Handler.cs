using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

internal interface IUpdateOrderPriceHandler : IHandler
{
    Task<Result<OrderResponse>> HandleAsync(UpdateOrderPriceRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateOrderPriceHandler(
    OrderDbContext context,
    ILogger<UpdateOrderPriceHandler> logger)
    : IUpdateOrderPriceHandler
{
    public async Task<Result<OrderResponse>> HandleAsync(UpdateOrderPriceRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating order with Id: {OrderId}, to new price", request.orderId);

        var order = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.orderId, cancellationToken);

        if (order == null)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result<OrderResponse>.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        order.TotalPrice = request.Price;
        order.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated order with Id: {OrderId}, to new price", request.orderId);

        return Result<OrderResponse>.Success(order.MapToResponse());
    }
}
