using ItemsShop.Catalog.Domain.Entities;
using ItemsShop.Catalog.Features.Shared.Responses;

namespace ItemsShop.Catalog.Features.Features.CartItems.GetCartItem;

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
