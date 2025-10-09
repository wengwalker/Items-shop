using ItemsShop.Catalog.Domain.Entities;
using ItemsShop.Catalog.Features.Shared.Responses;

namespace ItemsShop.Catalog.Features.Features.Categories.GetCategories;

internal static class GetCategoriesMappingExtensions
{
    public static GetCategoriesCommand MapToCommand(this GetCategoriesRequest request)
        => new(request.Name, request.OrderType);

    public static CategoryResponse MapToResponse(this CategoryEntity category)
        => new(category.Id,
                category.Name,
                category.Description);

    public static GetCategoriesResponse MapToResponse(this List<CategoryResponse> responses)
        => new(responses);
}
