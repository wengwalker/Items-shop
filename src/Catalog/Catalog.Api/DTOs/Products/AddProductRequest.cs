namespace Catalog.Api.DTOs.Products;

public record AddProductRequest(
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    Guid CategoryId);
