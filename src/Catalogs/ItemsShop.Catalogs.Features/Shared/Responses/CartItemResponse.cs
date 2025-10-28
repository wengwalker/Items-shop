namespace ItemsShop.Catalog.Features.Shared.Responses;
public sealed record CartItemResponse(
    Guid Id,
    int Quantity,
    Guid CartId,
    Guid ProductId);
