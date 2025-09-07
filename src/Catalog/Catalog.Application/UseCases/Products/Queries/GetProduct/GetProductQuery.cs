using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Products.Queries.GetProduct;

public record GetProductQuery(
    Guid ProductId)
    : IRequest<GetProductQueryResponse>;
