namespace Order.Application.UseCases.Orders.UpdateOrderPrice;

public record UpdateOrderPriceCommandResponse(
    Guid OrderId,
    decimal NewPrice,
    DateTime UpdatedAt);
