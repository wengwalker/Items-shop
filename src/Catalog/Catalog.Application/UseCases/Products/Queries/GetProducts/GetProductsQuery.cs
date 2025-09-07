using Domain.Common.Enums;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Products.Queries.GetProducts;

public record GetProductsQuery(
    string? Name,
    OrderQueryType? OrderType)
    : IRequest<GetProductsQueryResponse>;
