using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

internal static class GetCartMappingExtensions
{
    public static GetCartQuery MapToCommand(this GetCartRequest request)
        => new (request.cartId);

    public static GetCartResponse MapToResponse(this CartEntity cart)
        => new (cart.Id, cart.LastUpdated);
}
