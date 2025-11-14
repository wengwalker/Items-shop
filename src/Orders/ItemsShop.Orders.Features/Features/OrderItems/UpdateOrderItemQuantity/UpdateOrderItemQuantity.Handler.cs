using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

public sealed record UpdateOrderItemQuantityCommand(
    Guid OrderId,
    Guid ItemId,
    int Quantity) : IRequest<Result<UpdateOrderItemQuantityResponse>>;

public sealed record UpdateOrderItemQuantityResponse(
    Guid ItemId,
    Guid OrderId,
    int Quantity,
    Guid ProductId);

public sealed class UpdateOrderItemQuantityHandler
{
}
