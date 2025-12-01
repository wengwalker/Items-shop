using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

internal static class GetCartItemMappingExtensions
{
    public static CartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId);
}
