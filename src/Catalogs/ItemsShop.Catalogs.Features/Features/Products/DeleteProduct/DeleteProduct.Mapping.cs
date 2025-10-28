namespace ItemsShop.Catalog.Features.Features.Products.DeleteProduct;

internal static class DeleteProductMappingExtensions
{
    public static DeleteProductCommand MapToCommand(this DeleteProductRequest request)
        => new(request.id);
}
