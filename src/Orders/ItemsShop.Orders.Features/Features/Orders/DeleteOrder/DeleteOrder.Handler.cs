using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.DeleteOrder;

public sealed record DeleteOrderCommand(Guid OrderId) : IRequest<Result<DeleteOrderResponse>>;

public sealed record DeleteOrderResponse();

public sealed class DeleteOrderHandler(
    OrderDbContext context,
    ILogger<DeleteOrderHandler> logger) : IRequestHandler<DeleteOrderCommand, Result<DeleteOrderResponse>>
{
    public async Task<Result<DeleteOrderResponse>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting order with Id: {OrderId}", request.OrderId);

        var order = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<DeleteOrderResponse>
                .Failure($"Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted order with Id: {OrderId}", order.Id);

        return Result<DeleteOrderResponse>.Success();
    }
}
