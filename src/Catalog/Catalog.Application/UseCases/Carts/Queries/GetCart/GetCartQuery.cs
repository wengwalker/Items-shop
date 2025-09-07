using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Carts.Queries.GetCart;

public record GetCartQuery(
    Guid Id)
    : IRequest<GetCartQueryResponse>;
