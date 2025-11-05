namespace ItemsShop.Catalogs.Features.Features.Carts.DeleteCart;

internal static class DeleteCartMappingExtensions
{
    public static DeleteCartCommand MapToCommand(this DeleteCartRequest request)
        => new(request.cartId);
}
