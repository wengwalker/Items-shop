using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Categories.UpdateCategoryName;

internal static class UpdateCategoryNameMappingExtensions
{
    public static UpdateCategoryNameCommand MapToCommand(this UpdateCategoryNameRequest request, Guid categoryId)
        => new(categoryId, request.Name);

    public static UpdateCategoryNameResponse MapToResponse(this CategoryEntity category)
        => new(category.Id, category.Name);
}
