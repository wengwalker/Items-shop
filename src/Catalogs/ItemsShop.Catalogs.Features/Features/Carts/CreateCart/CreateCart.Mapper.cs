using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Carts.CreateCart;

internal static class CreateCartMappingExtensions
{
    public static CartResponse MapToResponse(this CartEntity cart)
        => new(cart.Id, cart.LastUpdated);
}
