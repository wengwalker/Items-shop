using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Application.Extensions;
using ItemsShop.Orders.Domain.Entities;

namespace ItemsShop.Orders.Features.Features.Orders.CreateOrder;

internal static class CreateOrderMappingExtensions
{
    public static OrderEntity MapToOrder(this CreateOrderCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            Status = (byte)OrderStatus.Draft,
            TotalPrice = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public static CreateOrderResponse MapToResponse(this OrderEntity order)
        => new(order.Id,
                order.Status.MapValueToOrderStatus(),
                order.TotalPrice,
                order.CreatedAt,
                order.UpdatedAt);
}
