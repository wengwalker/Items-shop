namespace Catalog.Application.UseCases.Products.Commands.UpdateProduct;

public record UpdateProductCommandResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
