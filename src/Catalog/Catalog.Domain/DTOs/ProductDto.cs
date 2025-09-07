namespace Catalog.Domain.DTOs;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
