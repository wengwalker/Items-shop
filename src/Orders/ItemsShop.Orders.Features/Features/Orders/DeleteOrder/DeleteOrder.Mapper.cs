namespace ItemsShop.Orders.Features.Features.Orders.DeleteOrder;

internal static class DeleteOrderMappingExtensions
{
    public static DeleteOrderCommand MapToCommand(this DeleteOrderRequest request)
        => new(request.orderId);
}
