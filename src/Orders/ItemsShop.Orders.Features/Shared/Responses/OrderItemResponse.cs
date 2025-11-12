namespace ItemsShop.Orders.Features.Shared.Responses;

public sealed record OrderItemResponse(
    Guid Id,
    Guid ProductId,
    decimal ProductPrice,
    int ProductQuantity,
    decimal ItemPrice);
