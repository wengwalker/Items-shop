using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

public sealed record GetOrderItemQuery(Guid OrderId, Guid ItemId) : IRequest<Result<GetOrderItemResponse>>;

public sealed record GetOrderItemResponse(OrderItemResponse Item);

public sealed class GetOrderItemHandler(
    OrderDbContext context,
    ILogger<GetOrderItemHandler> logger) : IRequestHandler<GetOrderItemQuery, Result<GetOrderItemResponse>>
{
    public async Task<Result<GetOrderItemResponse>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching OrderItem with Id {ItemId} from Order with Id {OrderId}", request.ItemId, request.OrderId);

        var orderExists = await context.Orders
            .AnyAsync(x => x.Id == request.OrderId, cancellationToken);

        if (!orderExists)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<GetOrderItemResponse>
                .Failure($"Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        var orderItem = await context.OrderItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ItemId && x.OrderId == request.OrderId, cancellationToken);

        if (orderItem == null)
        {
            logger.LogInformation("OrderItem with Id {ItemId} from Order with Id {OrderId} does not exists", request.ItemId, request.OrderId);

            return Result<GetOrderItemResponse>
                .Failure($"OrderItem with Id {request.ItemId} from Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        var response = orderItem.MapToResponse();

        logger.LogInformation("Fetched OrderItem with Id {ItemId} from Order with Id {OrderId}", request.ItemId, request.OrderId);

        return Result<GetOrderItemResponse>.Success(response);
    }
}
