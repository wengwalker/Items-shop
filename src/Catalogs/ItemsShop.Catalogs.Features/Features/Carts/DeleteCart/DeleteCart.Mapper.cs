namespace ItemsShop.Catalog.Features.Features.Carts.DeleteCart;

internal static class DeleteCartMappingExtensions
{
    public static DeleteCartCommand MapToCommand(this DeleteCartRequest request)
        => new(request.id);
}
