using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Products.Queries.GetProducts;

public record GetProductsQuery()
    : IRequest<GetProductsQueryResponse>;
