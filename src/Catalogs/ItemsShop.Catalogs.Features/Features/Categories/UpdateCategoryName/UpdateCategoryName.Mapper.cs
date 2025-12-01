using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

internal static class UpdateCategoryNameMappingExtensions
{
    public static CategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);
}
