using Order.Domain.Enums;

namespace Order.Application.UseCases.Orders.UpdateOrderStatus;

public record UpdateOrderStatusCommandResponse(
    Guid OrderId,
    OrderStatus NewStatus,
    DateTime UpdatedAt);
