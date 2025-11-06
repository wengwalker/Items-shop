using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.Orders.CreateOrder;

public sealed record CreateOrderCommand() : IRequest<Result<CreateOrderResponse>>;

public sealed record CreateOrderResponse(
    Guid OrderId,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAt,
    DateTime UpdatedAt);

public sealed class CreateOrderHandler(
    OrderDbContext context,
    ILogger<CreateOrderHandler> logger): IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating order entity");

        var order = request.MapToOrder();

        context.Orders.Add(order);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created order entity with Id {OrderId}", order.Id);

        var response = order.MapToResponse();

        return Result<CreateOrderResponse>.Success(response);
    }
}
