using Order.Domain.Enums;

namespace Order.Application.UseCases.Orders.ListOrders;

public record GetOrderQueryResponse(
    Guid OrderId,
    OrderStatus Status,
    decimal Price,
    DateTime UpdatedAt,
    DateTime CreatedAt);
