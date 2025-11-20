using ItemsShop.Catalogs.PublicApi;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

public sealed record CreateOrderItemCommand(
    Guid OrderId,
    long Quantity,
    Guid ProductId) : IRequest<Result<CreateOrderItemResponse>>;

public sealed record CreateOrderItemResponse(
    Guid Id,
    Guid OrderId,
    long Quantity,
    Guid ProductId,
    decimal ProductPrice,
    decimal ItemPrice);

public sealed class CreateOrderItemHandler(
    OrderDbContext context,
    ICatalogModuleApi catalogApi,
    ILogger<CreateOrderItemHandler> logger) : IRequestHandler<CreateOrderItemCommand, Result<CreateOrderItemResponse>>
{
    public async Task<Result<CreateOrderItemResponse>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating OrderItem that has Product with Id {ProductId} and it's quantity {Quantity} in Order with Id {OrderId}",
            request.ProductId, request.Quantity, request.OrderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<CreateOrderItemResponse>
                .Failure($"Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        var product = await catalogApi.GetProductAsync(request.MapToRequest(), cancellationToken);

        if (product.IsFailure || product.Value is null)
        {
            logger.LogInformation("Fetching Product with Id {ProductId} failed", request.ProductId);

            return Result<CreateOrderItemResponse>
                .Failure($"Fetching Product with Id {request.ProductId} failed", StatusCodes.Status400BadRequest);
        }

        if (request.Quantity > product.Value.Product.Quantity)
        {
            logger.LogInformation("The requested Product quantity is missing: Product with Id {ProductId} has {Quantity} Quantity, but requested {Quantity}",
                request.ProductId, product.Value.Product.Quantity, request.Quantity);

            return Result<CreateOrderItemResponse>
                .Failure($"The requested Product quantity is missing: Product with Id {request.ProductId} has {product.Value.Product.Quantity} Quantity, but requested {request.Quantity}", StatusCodes.Status400BadRequest);
        }

        var orderItem = request.MapToOrderItem(product.Value.Product);

        context.OrderItems.Add(orderItem);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created OrderItem that has Product with Id {ProductId} and it's quantity {Quantity} in Order with Id {OrderId}",
            request.ProductId, request.Quantity, request.OrderId);

        var response = orderItem.MapToResponse();

        return Result<CreateOrderItemResponse>.Success(response);
    }
}
