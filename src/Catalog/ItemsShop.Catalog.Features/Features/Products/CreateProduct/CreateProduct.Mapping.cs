using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Products.CreateProduct;

internal static class CreateProductMappingExtensions
{
    public static CreateProductCommand MapToCommand(this CreateProductRequest request)
        => new(request.Name,
                request.Description,
                request.Price,
                request.StockQuantity,
                request.CategoryId);

    public static ProductEntity MapToProduct(this CreateProductCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            StockQuantity = command.StockQuantity,
            CategoryId = command.CategoryId
        };

    public static CreateProductResponse MapToResponse(this ProductEntity product)
        => new(product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.StockQuantity,
                product.CreatedAt,
                product.UpdatedAt,
                product.CategoryId);
}
