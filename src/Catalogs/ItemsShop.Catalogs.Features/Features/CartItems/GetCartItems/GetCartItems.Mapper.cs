using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

internal static class GetCartItemsMappingExtensions
{
    public static GetCartItemsCommand MapToCommand(this GetCartItemsRequest request)
        => new (request.cartId);

    public static GetCartItemsResponse MapToResponse(this List<CartItemResponse> items)
        => new (items);
}
