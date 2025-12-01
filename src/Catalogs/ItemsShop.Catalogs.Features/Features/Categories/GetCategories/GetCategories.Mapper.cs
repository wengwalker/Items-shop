using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

internal static class GetCategoriesMappingExtensions
{
    public static CategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);
}
