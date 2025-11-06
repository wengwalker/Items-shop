using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

public sealed record UpdateOrderPriceCommand(
    Guid OrderId,
    decimal Price) : IRequest<Result<UpdateOrderPriceResponse>>;

public sealed record UpdateOrderPriceResponse(
    Guid OrderId,
    DateTime UpdatedAt,
    decimal Price);

public sealed class UpdateOrderPriceHandler(
    OrderDbContext context,
    ILogger<UpdateOrderPriceHandler> logger) : IRequestHandler<UpdateOrderPriceCommand, Result<UpdateOrderPriceResponse>>
{
    public async Task<Result<UpdateOrderPriceResponse>> Handle(UpdateOrderPriceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating order with Id: {OrderId}, to new price", request.OrderId);

        var order = await context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            logger.LogInformation("Order with Id {OrderId} does not exists", request.OrderId);

            return Result<UpdateOrderPriceResponse>
                .Failure($"Order with Id {request.OrderId} does not exists", StatusCodes.Status404NotFound);
        }

        order.TotalPrice = request.Price;
        order.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated order with Id: {OrderId}, to new price", request.OrderId);

        var response = order.MapToResponse();

        return Result<UpdateOrderPriceResponse>.Success(response);
    }
}
