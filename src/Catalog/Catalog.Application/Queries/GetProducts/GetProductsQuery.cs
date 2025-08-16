using Mediator.Lite.Interfaces;

namespace Catalog.Application.Queries.GetProducts;

public record GetProductsQuery() : IRequest<GetProductsQueryResponse>;
