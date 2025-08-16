using Catalog.Domain.DTOs;

namespace Catalog.Application.Queries.GetProducts;

public record GetProductsQueryResponse(List<ProductDto> Products);
