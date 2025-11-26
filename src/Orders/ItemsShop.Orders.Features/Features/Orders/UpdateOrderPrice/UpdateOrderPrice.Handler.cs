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
        logger.LogInformation("Updating order with Id: {OrderId}, to new price", request.OrderId);

        var order = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<OrderResponse>.Failure($"Order with Id {request.OrderId} does not exists", ErrorType.NotFound);
        }

        order.TotalPrice = request.Price;
        order.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated order with Id: {OrderId}, to new price", request.OrderId);

        return Result<OrderResponse>.Success(order.MapToResponse());
    }
}
