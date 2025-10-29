using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProducts;

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

    public static GetProductsResponse MapToResponse(this ICollection<ProductResponse> responses)
        => new(responses);
}
