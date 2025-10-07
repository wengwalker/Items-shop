using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductCategory;

internal static class UpdateProductCategoryMappingExtensions
{
    public static UpdateProductCategoryCommand MapToCommand(this UpdateProductCategoryRequest request, Guid productId)
        => new(productId, request.CategoryId);

    public static UpdateProductCategoryResponse MapToResponse(this ProductEntity product)
        => new(product.Id, product.CategoryId);
}
