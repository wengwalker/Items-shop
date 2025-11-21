using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.PublicApi.Contracts;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

internal static class UpdateProductQuantityMappingExtensions
{
    public static ProductResponse MapToResponse(this ProductEntity product)
        => new(product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity,
                product.CreatedAt,
                product.UpdatedAt,
                product.CategoryId);
}
