using Catalog.Domain.DTOs;

namespace Catalog.Application.UseCases.Products.Queries.GetProducts;

public record GetProductsQueryResponse(List<ProductDto> Products);
