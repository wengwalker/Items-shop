using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

internal static class GetCartMappingExtensions
{
    public static GetCartCommand MapToCommand(this GetCartRequest request)
        => new (request.id);

    public static GetCartResponse MapToResponse(this CartEntity cart)
        => new (cart.Id, cart.LastUpdated);
}
