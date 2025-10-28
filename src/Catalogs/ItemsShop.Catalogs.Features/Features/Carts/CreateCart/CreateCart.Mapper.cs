using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Carts.CreateCart;

internal static class CreateCartMappingExtensions
{
    public static CartEntity MapToCart(this CreateCartCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow,
        };

    public static CreateCartResponse MapToResponse(this CartEntity cart)
        => new(cart.Id, cart.LastUpdated);
}
