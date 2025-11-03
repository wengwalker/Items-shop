namespace ItemsShop.Catalogs.Features.Features.Categories.DeleteCategory;

internal static class DeleteCategoryMappingExtensions
{
    public static DeleteCategoryCommand MapToCommand(this DeleteCategoryRequest request)
        => new(request.categoryId);
}
