using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

internal static class GetCartItemsMappingExtensions
{
    public static GetCartItemsQuery MapToCommand(this GetCartItemsRequest request)
        => new (request.cartId);

    public static GetCartItemsResponse MapToResponse(this ICollection<CartItemResponse> items)
        => new (items);
}
