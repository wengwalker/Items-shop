using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

internal static class GetCategoriesMappingExtensions
{
    public static GetCategoriesCommand MapToCommand(this GetCategoriesRequest request)
        => new(request.Name, request.OrderType);

    public static CategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);

    public static GetCategoriesResponse MapToResponse(this ICollection<CategoryResponse> responses)
        => new(responses);
}
