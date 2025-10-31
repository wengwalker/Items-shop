using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

internal static class GetProductMappingExtensions
{
    public static GetProductQuery MapToCommand(this GetProductRequest request)
        => new(request.id);

    public static GetProductResponse MapToResponse(this ProductEntity product)
        => new (new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity,
                product.CreatedAt,
                product.UpdatedAt,
                product.CategoryId)
            );
}
