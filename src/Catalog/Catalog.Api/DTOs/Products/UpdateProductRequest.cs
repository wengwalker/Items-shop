namespace Catalog.Api.DTOs.Products;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    long StockQuantity);
