using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Products.CreateProduct;

internal static class CreateProductMappingExtensions
{
    public static CreateProductCommand MapToCommand(this CreateProductRequest request)
        => new(request.Name,
                request.Description,
                request.Price,
                request.Quantity,
                request.CategoryId);

    public static ProductEntity MapToProduct(this CreateProductCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Quantity = command.Quantity,
            CategoryId = command.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public static CreateProductResponse MapToResponse(this ProductEntity product)
        => new(product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Quantity,
                product.CreatedAt,
                product.UpdatedAt,
                product.CategoryId);
}
