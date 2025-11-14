using ItemsShop.Common.Domain.Results;
using ItemsShop.Orders.Infrastructure.Database;
using Mediator.Lite.Interfaces;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

public sealed record CreateOrderItemCommand(
    Guid OrderId,
    int Quantity,
    Guid ProductId) : IRequest<Result<CreateOrderItemResponse>>;

public sealed record CreateOrderItemResponse(
    Guid Id,
    Guid OrderId,
    int Quantity,
    Guid ProductId,
    decimal ProductPrice,
    decimal ItemPrice);

/*public sealed class CreateOrderItemHandler(
    OrderDbContext context,
    ILogger<CreateOrderItemHandler> logger) : IRequestHandler<CreateOrderItemCommand, Result<CreateOrderItemResponse>>
{
    public async Task<Result<CreateOrderItemResponse>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        // 
    }
}*/
