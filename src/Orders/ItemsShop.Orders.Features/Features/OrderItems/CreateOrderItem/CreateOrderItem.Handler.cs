using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

internal interface ICreateOrderItemHandler : IHandler
{
    Task<Result<OrderItemResponse>> HandleAsync(CreateOrderItemRequest request, CancellationToken cancellationToken);
}

internal sealed class CreateOrderItemHandler(
    OrderDbContext context,
    ICatalogModuleApi catalogApi,
    ILogger<CreateOrderItemHandler> logger)
    : ICreateOrderItemHandler
{
    public async Task<Result<OrderItemResponse>> HandleAsync(CreateOrderItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating OrderItem that has Product with Id {ProductId} and it's quantity {Quantity} in Order with Id {OrderId}",
            request.ProductId, request.Quantity, request.OrderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<OrderItemResponse>.Failure($"Order with Id {request.OrderId} does not exists", ErrorType.NotFound);
        }

        var product = await catalogApi.GetProductAsync(request.MapToRequest(), cancellationToken);

        if (product.IsFailure || product.Value is null)
        {
            logger.LogInformation("Fetching Product with Id {ProductId} failed", request.ProductId);

            return Result<OrderItemResponse>.Failure($"Fetching Product with Id {request.ProductId} failed", ErrorType.BadRequest);
        }

        if (request.Quantity > product.Value.Quantity)
        {
            logger.LogInformation("The requested Product quantity is missing: Product with Id {ProductId} has {Quantity} Quantity, but requested {Quantity}",
                request.ProductId, product.Value.Quantity, request.Quantity);

            return Result<OrderItemResponse>
                .Failure($"The requested Product quantity is missing: Product with Id {request.ProductId} has {product.Value.Quantity} Quantity, but requested {request.Quantity}",
                    ErrorType.BadRequest);
        }

        var orderItem = request.MapToOrderItem(product.Value);

        context.OrderItems.Add(orderItem);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created OrderItem that has Product with Id {ProductId} and it's quantity {Quantity} in Order with Id {OrderId}",
            request.ProductId, request.Quantity, request.OrderId);

        return Result<OrderItemResponse>.Success(orderItem.MapToResponse());
    }
}
