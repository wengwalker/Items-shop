using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.PublicApi.Contracts;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

internal static class GetProductMappingExtensions
{
    public static GetProductResponse MapToResponse(this ProductEntity product)
        => new (new ProductItem(
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
