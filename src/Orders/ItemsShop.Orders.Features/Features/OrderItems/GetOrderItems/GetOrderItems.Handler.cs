using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

public sealed record GetOrderItemsQuery(Guid OrderId) : IRequest<Result<GetOrderItemsResponse>>;

public sealed record GetOrderItemsResponse(ICollection<OrderItemResponse> Items);

public sealed class GetOrderItemsHandler(
    OrderDbContext context,
    ILogger<GetOrderItemsHandler> logger) : IRequestHandler<GetOrderItemsQuery, Result<GetOrderItemsResponse>>
{
    public async Task<Result<GetOrderItemsResponse>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching OrderItems from Order with Id {OrderId}", request.OrderId);

        bool orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<GetOrderItemsResponse>
                .Failure($"Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        List<OrderItemResponse> orderItems = await context.OrderItems
            .AsNoTracking()
            .Where(x => x.OrderId == request.OrderId)
            .Select(x => new OrderItemResponse(x.Id, x.ProductId, x.ProductPrice, x.ProductQuantity, x.ItemPrice))
            .ToListAsync(cancellationToken);

        GetOrderItemsResponse response = orderItems.MapToResponse();

        logger.LogInformation("Fetched OrderItems from Order with Id {OrderId}", request.OrderId);

        return Result<GetOrderItemsResponse>.Success(response);
    }
}
