using ItemsShop.Catalog.Domain.Entities;
using ItemsShop.Catalog.Features.Shared.Responses;

namespace ItemsShop.Catalog.Features.Features.Products.GetProduct;

internal static class GetProductMappingExtensions
{
    public static GetProductCommand MapToCommand(this GetProductRequest request)
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
