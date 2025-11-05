using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

internal static class UpdateCartItemQuantityMappingExtensions
{
    public static UpdateCartItemQuantityCommand MapToCommand(this UpdateCartItemQuantityRequest request, Guid CartId, Guid ItemId)
        => new (CartId, ItemId, request.Quantity);

    public static UpdateCartItemQuantityResponse MapToResponse(this CartItemEntity cartItem)
        => new(cartItem.Id,
                cartItem.CartId,
                cartItem.Quantity,
                cartItem.ProductId);
}
