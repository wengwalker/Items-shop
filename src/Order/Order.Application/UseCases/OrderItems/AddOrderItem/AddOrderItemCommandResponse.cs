namespace Order.Application.UseCases.OrderItems.AddOrderItem;

public record AddOrderItemCommandResponse(
    Guid OrderItemId,
    Guid ProductItemId,
    decimal ProductItemPrice,
    decimal ItemsQuantity,
    decimal ItemsPrice,
    Guid OrderId);
