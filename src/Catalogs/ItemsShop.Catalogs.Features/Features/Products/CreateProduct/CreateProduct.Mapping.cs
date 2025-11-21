using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.PublicApi.Contracts;

namespace ItemsShop.Catalogs.Features.Features.Products.CreateProduct;

internal static class CreateProductMappingExtensions
{
    public static ProductEntity MapToProduct(this CreateProductRequest request)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

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
