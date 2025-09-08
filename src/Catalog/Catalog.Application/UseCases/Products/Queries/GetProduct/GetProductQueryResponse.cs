namespace Catalog.Application.UseCases.Products.Queries.GetProduct;

public record GetProductQueryResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    DateTime CreatedAt,
    Guid CategoryId);
