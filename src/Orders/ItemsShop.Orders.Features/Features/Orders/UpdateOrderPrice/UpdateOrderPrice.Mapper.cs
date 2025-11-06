using ItemsShop.Orders.Domain.Entities;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

internal static class UpdateOrderPriceMappingExtensions
{
    public static UpdateOrderPriceCommand MapToCommand(this UpdateOrderPriceRequest request, Guid orderId)
        => new(orderId, request.Price);

    public static UpdateOrderPriceResponse MapToResponse(this OrderEntity order)
        => new(order.Id,
                order.UpdatedAt,
                order.TotalPrice);

}
