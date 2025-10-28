using ItemsShop.Catalog.Domain.Entities;

namespace ItemsShop.Catalog.Features.Features.Categories.UpdateCategoryDescription;
internal static class UpdateCategoryDescriptionMappingExtensions
{
    public static UpdateCategoryDescriptionCommand MapToCommand(this UpdateCategoryDescriptionRequest request, Guid categoryId)
        => new(categoryId, request.Description);

    public static UpdateCategoryDescriptionResponse MapToResponse(this CategoryEntity category)
        => new(category.Id, category.Description);
}
