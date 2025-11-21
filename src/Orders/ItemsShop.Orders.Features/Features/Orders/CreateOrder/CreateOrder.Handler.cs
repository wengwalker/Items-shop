using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.CreateOrder;

internal interface ICreateOrderHandler : IHandler
{
    Task<Result<OrderResponse>> HandleAsync(CancellationToken cancellationToken);
}

internal sealed class CreateOrderHandler(
    OrderDbContext context,
    ILogger<CreateOrderHandler> logger)
    : ICreateOrderHandler
{
    public async Task<Result<OrderResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating order entity");

        var order = new OrderEntity()
        {
            Id = Guid.NewGuid(),
            Status = (byte)OrderStatus.Draft,
            TotalPrice = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created order entity with Id {OrderId}", order.Id);

        return Result<OrderResponse>.Success(order.MapToResponse());
    }
}
