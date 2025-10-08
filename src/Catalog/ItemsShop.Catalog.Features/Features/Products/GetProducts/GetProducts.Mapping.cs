using ItemsShop.Catalog.Domain.Entities;
using ItemsShop.Catalog.Features.Shared.Responses;

namespace ItemsShop.Catalog.Features.Features.Products.GetProducts;

internal static class GetProductsMappingExtensions
{
    public static GetProductsCommand MapToCommand(this GetProductsRequest request)
        => new(request.Name, request.OrderType);

    public static ProductResponse MapToResponse(this ProductEntity product)
        => new(product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Quantity,
            product.CreatedAt,
            product.UpdatedAt,
            product.CategoryId);

    public static GetProductsResponse MapToResponse(this List<ProductResponse> responses)
        => new(responses);
}
