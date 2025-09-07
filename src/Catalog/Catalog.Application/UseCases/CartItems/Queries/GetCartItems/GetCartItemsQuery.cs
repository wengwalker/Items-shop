using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.CartItems.Queries.GetCartItems;

public record GetCartItemsQuery(
    Guid CartId)
    : IRequest<GetCartItemsQueryResponse>;
