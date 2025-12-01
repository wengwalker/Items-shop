namespace ItemsShop.Catalogs.PublicApi.Contracts;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);
