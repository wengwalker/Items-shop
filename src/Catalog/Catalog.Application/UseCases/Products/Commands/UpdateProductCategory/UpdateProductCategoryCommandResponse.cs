namespace Catalog.Application.UseCases.Products.Commands.UpdateProductCategory;

public record UpdateProductCategoryCommandResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
