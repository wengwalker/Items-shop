namespace ItemsShop.Orders.Features.Shared.Responses;

public sealed record OrderItemResponse(
    Guid Id,
    Guid ProductId,
    decimal ProductPrice,
    long ProductQuantity,
    decimal ItemPrice);
