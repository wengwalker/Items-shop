namespace Catalog.Application.Commands.AddProduct;

public record AddProductCommandResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
