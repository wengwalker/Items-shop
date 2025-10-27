using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Carts.GetCart;

internal static class GetCartMappingExtensions
{
    public static GetCartCommand MapToCommand(this GetCartRequest request)
        => new (request.id);

    public static GetCartResponse MapToResponse(this CartEntity cart)
        => new (cart.Id, cart.LastUpdated);
}
