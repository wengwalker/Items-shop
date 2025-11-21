using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

internal interface IUpdateOrderItemQuantityHandler : IHandler
{
    Task<Result<OrderItemResponse>> HandleAsync(UpdateOrderItemQuantityRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateOrderItemQuantityHandler(
    OrderDbContext context,
    ICatalogModuleApi catalogApi,
    ILogger<UpdateOrderItemQuantityHandler> logger)
    : IUpdateOrderItemQuantityHandler
{
    public async Task<Result<OrderItemResponse>> HandleAsync(UpdateOrderItemQuantityRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating OrderItem quantity with Id {ItemId} to quantity {Quantity} in Order with Id {OrderId}",
            request.itemId, request.Quantity, request.orderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.orderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.orderId);

            return Result<OrderItemResponse>.Failure($"Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        var orderItem = await context.OrderItems
            .FirstOrDefaultAsync(x => x.Id == request.itemId && x.OrderId == request.orderId, cancellationToken);

        if (orderItem == null)
        {
            logger.LogInformation("OrderItem with Id {ItemId} from Order with Id {OrderId} does not exists", request.itemId, request.orderId);

            return Result<OrderItemResponse>.Failure($"OrderItem with Id {request.itemId} from Order with Id {request.orderId} does not exists", ErrorType.NotFound);
        }

        var product = await catalogApi.GetProductAsync(orderItem.MapToRequest(), cancellationToken);

        if (product.IsFailure || product.Value is null)
        {
            logger.LogInformation("Fetching Product with Id {ProductId} failed", orderItem.ProductId);

            return Result<OrderItemResponse>.Failure($"Fetching Product with Id {orderItem.ProductId} failed", ErrorType.BadRequest);
        }

        if (request.Quantity > product.Value.Quantity)
        {
            logger.LogInformation("The requested Product quantity is missing: Product with Id {ProductId} has {Quantity} Quantity, but requested {Quantity}",
                orderItem.ProductId, product.Value.Quantity, request.Quantity);

            return Result<OrderItemResponse>
                .Failure($"The requested Product quantity is missing: Product with Id {orderItem.ProductId} has {product.Value.Quantity} Quantity, but requested {request.Quantity}",
                    ErrorType.BadRequest);
        }

        orderItem.ProductQuantity = request.Quantity;
        orderItem.ItemPrice = request.Quantity * product.Value.Price;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated OrderItem quantity with Id {ItemId} to quantity {Quantity} in Order with Id {OrderId}",
            request.itemId, request.Quantity, request.orderId);

        return Result<OrderItemResponse>.Success(orderItem.MapToResponse());
    }
}
