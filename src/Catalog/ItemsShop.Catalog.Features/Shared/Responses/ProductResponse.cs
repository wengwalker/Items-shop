namespace ItemsShop.Catalog.Features.Shared.Responses;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);
