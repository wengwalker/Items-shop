using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

internal static class CreateOrderItemMappingExtensions
{
    public static GetProductRequest MapToRequest(this CreateOrderItemRequest request)
        => new(request.ProductId);

    public static OrderItemEntity MapToOrderItem(this CreateOrderItemRequest request, ProductResponse product)
        => new()
        {
            Id = Guid.NewGuid(),
            OrderId = request.OrderId,
            ProductId = request.ProductId,
            ProductPrice = product.Price,
            ProductQuantity = request.Quantity,
            ItemPrice = product.Price * request.Quantity,
        };

    public static OrderItemResponse MapToResponse(this OrderItemEntity orderItem)
        => new(orderItem.Id,
                orderItem.ProductId,
                orderItem.ProductPrice,
                orderItem.ProductQuantity,
                orderItem.ItemPrice,
                orderItem.OrderId);
}
