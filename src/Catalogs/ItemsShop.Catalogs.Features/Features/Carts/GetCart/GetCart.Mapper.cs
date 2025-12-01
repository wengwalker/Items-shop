using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

internal static class GetCartMappingExtensions
{
    public static CartResponse MapToResponse(this CartEntity cart)
        => new(cart.Id, cart.LastUpdated);
}
