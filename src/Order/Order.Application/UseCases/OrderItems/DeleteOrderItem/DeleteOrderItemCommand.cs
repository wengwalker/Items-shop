using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.OrderItems.DeleteOrderItem;

public record DeleteOrderItemCommand(
    Guid OrderId,
    Guid OrderItemId) : IRequest;
