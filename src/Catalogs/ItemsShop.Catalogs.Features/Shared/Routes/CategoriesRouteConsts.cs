namespace ItemsShop.Catalog.Features.Shared.Routes;

internal static class CategoriesRouteConsts
{
    internal const string BaseRoute = "/api/v1/categories";

    internal const string GetCategory = BaseRoute + "/{id:guid}";

    internal const string DeleteCategory = BaseRoute + "/{id:guid}";

    internal const string UpdateCategoryName = BaseRoute + "/{id:guid}/name";

    internal const string UpdateCategoryDescription = BaseRoute + "/{id:guid}/description";
}
