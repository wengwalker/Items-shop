namespace ItemsShop.Catalog.Features.Shared.Responses;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);
