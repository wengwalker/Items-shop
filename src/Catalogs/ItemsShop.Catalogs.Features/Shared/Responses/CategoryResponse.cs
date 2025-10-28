namespace ItemsShop.Catalogs.Features.Shared.Responses;

public sealed record CategoryResponse(
    Guid Id,
    string Name,
    string? Description);
