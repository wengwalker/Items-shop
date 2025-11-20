namespace ItemsShop.Catalogs.PublicApi.Contracts;

public sealed record ProductItem(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);
