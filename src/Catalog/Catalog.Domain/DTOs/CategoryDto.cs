namespace Catalog.Domain.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Description);
