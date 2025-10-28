namespace ItemsShop.Catalog.Features.Features.Categories.DeleteCategory;

internal static class DeleteCategoryMappingExtensions
{
    public static DeleteCategoryCommand MapToCommand(this DeleteCategoryRequest request)
        => new(request.id);
}
