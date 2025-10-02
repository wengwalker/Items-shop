using Order.Domain.Enums;

namespace Order.Application.UseCases.Orders.GetOrder;

public record GetOrderQueryResponse(
    Guid OrderId,
    OrderStatus Status,
    decimal Price,
    DateTime UpdatedAt,
    DateTime CreatedAt);
