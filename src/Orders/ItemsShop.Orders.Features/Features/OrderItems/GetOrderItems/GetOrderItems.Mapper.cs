using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

internal static class GetOrderItemsMappingExtensions
{
    public static GetOrderItemsQuery MapToCommand(this GetOrderItemsRequest request)
        => new(request.orderId);

    public static GetOrderItemsResponse MapToResponse(this ICollection<OrderItemResponse> items)
        => new(items);
}
