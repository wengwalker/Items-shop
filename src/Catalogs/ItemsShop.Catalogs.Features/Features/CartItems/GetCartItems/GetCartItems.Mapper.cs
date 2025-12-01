using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

internal static class GetCartItemsMappingExtensions
{
    public static CartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId);
}
