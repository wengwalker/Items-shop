namespace Catalog.Application.Queries.GetProduct;

public record GetProductQueryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
