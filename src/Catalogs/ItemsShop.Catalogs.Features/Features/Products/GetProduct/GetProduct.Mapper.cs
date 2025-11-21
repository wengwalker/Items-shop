using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.PublicApi.Contracts;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

internal static class GetProductMappingExtensions
{
    public static GetProductRequest MapToRequest(this Guid productId)
        => new (productId);

    public static ProductResponse MapToResponse(this ProductEntity product)
        => new (product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity,
                product.CreatedAt,
                product.UpdatedAt,
                product.CategoryId);
}
