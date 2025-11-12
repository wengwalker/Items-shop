namespace ItemsShop.Catalogs.Features.Shared.Consts;

internal static class CategoriesRouteConsts
{
    internal const string BaseRoute = "/api/v1/categories";

    internal const string GetCategory = BaseRoute + "/{categoryId:guid}";

    internal const string DeleteCategory = BaseRoute + "/{categoryId:guid}";

    internal const string UpdateCategoryName = BaseRoute + "/{categoryId:guid}/name";

    internal const string UpdateCategoryDescription = BaseRoute + "/{categoryId:guid}/description";
}

internal static class CategoriesTagConsts
{
    internal static readonly string[] CategoriesEndpointTags = ["Categories"];
}
