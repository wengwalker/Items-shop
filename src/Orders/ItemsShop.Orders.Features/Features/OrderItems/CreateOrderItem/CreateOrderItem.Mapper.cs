namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

internal static class CreateOrderItemMappingExtensions
{
    public static CreateOrderItemCommand MapToCommand(this CreateOrderItemRequest request, Guid orderId)
        => new(orderId,
                request.Quantity,
                request.ProductId);
}
