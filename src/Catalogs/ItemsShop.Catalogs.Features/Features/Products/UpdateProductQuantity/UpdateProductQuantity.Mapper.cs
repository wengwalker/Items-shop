using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductQuantity;

internal static class UpdateProductQuantityMappingExtensions
{
    public static UpdateProductQuantityCommand MapToCommand(this UpdateProductQuantityRequest request, Guid productId)
        => new(productId, request.Quantity);

    public static UpdateProductQuantityResponse MapToResponse(this ProductEntity product)
        => new(product.Id, product.Quantity);
}
