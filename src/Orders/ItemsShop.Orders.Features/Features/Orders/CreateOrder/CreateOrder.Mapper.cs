using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Domain.Enums;

namespace ItemsShop.Orders.Features.Features.Orders.CreateOrder;

internal static class CreateOrderMappingExtensions
{
    public static OrderEntity MapToOrder(this CreateOrderCommand command)
        => new ()
        {
            Id = Guid.NewGuid(),
            Status = OrderStatus.Draft,
            TotalPrice = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public static CreateOrderResponse MapToResponse(this OrderEntity order)
        => new (order.Id,
                order.Status,
                order.TotalPrice,
                order.CreatedAt,
                order.UpdatedAt);
}
