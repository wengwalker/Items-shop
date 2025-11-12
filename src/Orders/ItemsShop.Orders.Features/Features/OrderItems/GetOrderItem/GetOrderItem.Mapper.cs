using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

internal static class GetOrderItemMappingExtensions
{
    public static GetOrderItemQuery MapToCommand(this GetOrderItemRequest request)
        => new(request.orderId, request.itemId);

    public static GetOrderItemResponse MapToResponse(this OrderItemEntity orderItem)
        => new(
            new OrderItemResponse(
                orderItem.Id,
                orderItem.ProductId,
                orderItem.ProductPrice,
                orderItem.ProductQuantity,
                orderItem.ItemPrice)
            );
}
