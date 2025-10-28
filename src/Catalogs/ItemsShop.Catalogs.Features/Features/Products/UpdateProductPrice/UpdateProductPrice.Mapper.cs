using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductPrice;

internal static class UpdateProductPriceMappingExtensions
{
    public static UpdateProductPriceCommand MapToCommand(this UpdateProductPriceRequest request, Guid productId)
        => new(productId, request.Price);

    public static UpdateProductPriceResponse MapToResponse(this ProductEntity product)
        => new(product.Id, product.Price);
}
