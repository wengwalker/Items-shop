namespace Catalog.Domain.DTOs;

public record CartItemDto(
    Guid Id,
    int Quantity,
    Guid CartId,
    Guid ProductId);
