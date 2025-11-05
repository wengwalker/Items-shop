namespace ItemsShop.Catalogs.Features.Features.CartItems.DeleteCartItem;

internal static class DeleteCartItemMappingExtensions
{
    public static DeleteCartItemCommand MapToCommand(this DeleteCartItemRequest request)
        => new (request.cartId, request.itemId);
}
