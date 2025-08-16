using Mediator.Lite.Interfaces;

namespace Catalog.Application.Queries.GetProduct;

public record GetProductQuery(
    Guid ProductId)
    : IRequest<GetProductQueryResponse>;
