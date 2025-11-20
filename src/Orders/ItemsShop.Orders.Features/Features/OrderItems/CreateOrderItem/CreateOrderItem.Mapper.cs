using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Orders.Domain.Entities;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

internal static class CreateOrderItemMappingExtensions
{
    public static CreateOrderItemCommand MapToCommand(this CreateOrderItemRequest request, Guid orderId)
        => new(orderId,
                request.Quantity,
                request.ProductId);

    public static GetProductRequest MapToRequest(this CreateOrderItemCommand command)
        => new(command.ProductId);

    public static OrderItemEntity MapToOrderItem(this CreateOrderItemCommand command, ProductItem product)
        => new()
        {
            Id = Guid.NewGuid(),
            OrderId = command.OrderId,
            ProductId = command.ProductId,
            ProductPrice = product.Price,
            ProductQuantity = product.Quantity,
            ItemPrice = product.Price * command.Quantity,
        };

    public static CreateOrderItemResponse MapToResponse(this OrderItemEntity orderItem)
        => new(orderItem.Id,
                orderItem.OrderId,
                orderItem.ProductQuantity,
                orderItem.ProductId,
                orderItem.ProductPrice,
                orderItem.ItemPrice);
}
