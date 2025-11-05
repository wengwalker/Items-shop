using ItemsShop.Catalogs.Domain.Entities;

namespace ItemsShop.Catalogs.Features.Features.Categories.CreateCategory;

internal static class CreateCategoryMappingExtensions
{
    public static CreateCategoryCommand MapToCommand(this CreateCategoryRequest request)
        => new(request.Name, request.Description);

    public static CategoryEntity MapToCategory(this CreateCategoryCommand command)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
        };

    public static CreateCategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);
}
