namespace ItemsShop.Catalogs.Features.Shared.Responses;

public sealed record CartItemResponse(
    Guid Id,
    int Quantity,
    Guid CartId,
    Guid ProductId);
