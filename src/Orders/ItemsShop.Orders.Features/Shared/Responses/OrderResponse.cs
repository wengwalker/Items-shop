using ItemsShop.Common.Application.Enums;

namespace ItemsShop.Orders.Features.Shared.Responses;

public sealed record OrderResponse(
    Guid Id,
    OrderStatus Status,
    decimal TotalPrice,
    DateTime CreatedAt,
    DateTime UpdatedAt);
