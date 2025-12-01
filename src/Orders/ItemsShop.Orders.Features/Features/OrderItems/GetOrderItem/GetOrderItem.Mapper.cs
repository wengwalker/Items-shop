using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

internal static class GetOrderItemMappingExtensions
{
    public static OrderItemResponse MapToResponse(this OrderItemEntity orderItem)
        => new(orderItem.Id,
                orderItem.ProductId,
                orderItem.ProductPrice,
                orderItem.ProductQuantity,
                orderItem.ItemPrice,
                orderItem.OrderId);
}
