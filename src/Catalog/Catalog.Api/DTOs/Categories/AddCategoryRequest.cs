namespace Catalog.Api.DTOs.Categories;

public record AddCategoryRequest(
    string Name,
    string? Description);
