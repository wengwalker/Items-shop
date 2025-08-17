namespace Catalog.Application.UseCases.Categories.Queries.GetCategory;

public record GetCategoryQueryResponse(
    Guid Id,
    string Name,
    string? Description);
