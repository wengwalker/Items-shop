namespace Catalog.Application.UseCases.Carts.Queries.GetCart;

public record GetCartQueryResponse(
    Guid Id,
    DateTime LastUpdated);
