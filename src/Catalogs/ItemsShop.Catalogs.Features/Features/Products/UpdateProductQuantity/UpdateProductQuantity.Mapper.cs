using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

internal static class UpdateProductQuantityMappingExtensions
{
    public static UpdateProductQuantityCommand MapToCommand(this UpdateProductQuantityRequest request, Guid productId)
        => new(productId, request.Quantity);

    public static UpdateProductQuantityResponse MapToResponse(this ProductEntity product)
        => new(product.Id, product.Quantity);
}
