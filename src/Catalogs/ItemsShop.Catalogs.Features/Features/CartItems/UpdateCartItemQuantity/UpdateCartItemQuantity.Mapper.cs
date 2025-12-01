using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

internal static class UpdateCartItemQuantityMappingExtensions
{
    public static CartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId);
}
