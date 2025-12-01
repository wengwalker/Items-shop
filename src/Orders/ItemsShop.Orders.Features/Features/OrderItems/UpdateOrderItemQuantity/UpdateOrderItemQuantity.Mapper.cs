using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

internal static class UpdateOrderItemQuantityMappingExtensions
{
    public static GetProductRequest MapToRequest(this OrderItemEntity orderItem)
        => new(orderItem.ProductId);

    public static OrderItemResponse MapToResponse(this OrderItemEntity orderItem)
        => new(orderItem.Id,
                orderItem.ProductId,
                orderItem.ProductPrice,
                orderItem.ProductQuantity,
                orderItem.ItemPrice,
                orderItem.OrderId);
}
