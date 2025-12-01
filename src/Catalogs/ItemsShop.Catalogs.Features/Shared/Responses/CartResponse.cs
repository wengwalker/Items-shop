namespace ItemsShop.Catalogs.Features.Shared.Responses;

public sealed record CartResponse(
    Guid CartId,
    DateTime LastUpdated);
