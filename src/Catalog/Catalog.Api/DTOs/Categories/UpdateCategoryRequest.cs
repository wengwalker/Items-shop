namespace Catalog.Api.DTOs.Categories;

public record UpdateCategoryRequest(
    string Name,
    string? Description);
