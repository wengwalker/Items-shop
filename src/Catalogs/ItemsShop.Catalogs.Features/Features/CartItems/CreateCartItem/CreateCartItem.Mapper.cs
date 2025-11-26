using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

internal static class CreateCartItemMappingExtensions
{
    public static CartItemEntity MapToCartItem(this CreateCartItemRequest request)
        => new()
        {
            Id = Guid.NewGuid(),
            CartId = request.CartId,
            Quantity = request.Quantity,
            ProductId = request.ProductId,
        };

    public static CartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId);
}
