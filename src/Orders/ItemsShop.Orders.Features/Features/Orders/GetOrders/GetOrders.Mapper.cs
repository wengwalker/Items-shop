using ItemsShop.Common.Application.Extensions;
using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

internal static class GetOrdersMappingExtensions
{
    public static OrderResponse MapToResponse(this OrderEntity order)
        => new(order.Id,
                order.Status.MapValueToOrderStatus(),
                order.TotalPrice,
                order.CreatedAt,
                order.UpdatedAt);
}
