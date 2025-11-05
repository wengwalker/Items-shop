using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

internal static class CreateCartItemMappingExtensions
{
    public static CreateCartItemCommand MapToCommand(this CreateCartItemRequest request, Guid cartId)
        => new(cartId,
                request.Quantity,
                request.ProductId);

    public static CartItemEntity MapToCartItem(this CreateCartItemCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            CartId = command.CartId,
            Quantity = command.Quantity,
            ProductId = command.ProductId,
        };

    public static CreateCartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId);
}
