namespace Catalog.Api.DTOs.CartItems;

public record AddCartItemRequest(
    int Quantity,
    Guid ProductId);
