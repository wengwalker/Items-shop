using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

internal static class GetCartItemMappingExtensions
{
    public static GetCartItemCommand MapToCommand(this GetCartItemRequest request)
        => new (request.cartId, request.itemId);

    public static GetCartItemResponse MapToResponse(this CartItemEntity cartItem)
        => new(
            new CartItemResponse(
                cartItem.Id,
                cartItem.Quantity,
                cartItem.CartId,
                cartItem.ProductId)
            );
}
