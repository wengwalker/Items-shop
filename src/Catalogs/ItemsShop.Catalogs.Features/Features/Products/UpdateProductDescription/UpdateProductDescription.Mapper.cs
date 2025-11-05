using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductDescription;

internal static class UpdateProductDescriptionMappingExtensions
{
    public static UpdateProductDescriptionCommand MapToCommand(this UpdateProductDescriptionRequest request, Guid productId)
        => new(productId, request.Description);

    public static UpdateProductDescriptionResponse MapToResponse(this ProductEntity product)
        => new(product.Id, product.Description);
}
