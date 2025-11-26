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
        logger.LogInformation("Fetching OrderItems from Order with Id {OrderId}", request.OrderId);

        bool orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<List<OrderItemResponse>>.Failure($"Order with Id {request.OrderId} does not exists", ErrorType.NotFound);
        }

        List<OrderItemResponse> orderItems = await context.OrderItems
            .AsNoTracking()
            .Where(x => x.OrderId == request.OrderId)
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        logger.LogInformation("Fetched OrderItems from Order with Id {OrderId}", request.OrderId);

        return Result<List<OrderItemResponse>>.Success(orderItems);
    }
}
