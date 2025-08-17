using Catalog.Domain.DTOs;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategories;

public record GetCategoriesQueryResponse(
    List<CategoryDto> Categories);
