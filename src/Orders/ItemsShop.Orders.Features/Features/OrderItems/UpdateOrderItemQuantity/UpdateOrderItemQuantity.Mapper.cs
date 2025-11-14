using ItemsShop.Orders.Domain.Entities;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

internal static class UpdateOrderItemQuantityMappingExtensions
{
    public static UpdateOrderItemQuantityCommand MapToCommand(this UpdateOrderItemQuantityRequest request, Guid OrderId, Guid ItemId)
        => new (OrderId, ItemId, request.Quantity);

    public static UpdateOrderItemQuantityResponse MapToResponse(this OrderItemEntity orderItem)
        => new (orderItem.Id,
                orderItem.OrderId,
                orderItem.ProductQuantity,
                orderItem.ProductId);
}
