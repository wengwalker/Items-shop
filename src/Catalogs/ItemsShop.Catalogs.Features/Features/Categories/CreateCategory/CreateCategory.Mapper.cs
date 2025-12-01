using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Categories.CreateCategory;

internal static class CreateCategoryMappingExtensions
{
    public static CategoryEntity MapToCategory(this CreateCategoryRequest request)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
        };

    public static CategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);
}
