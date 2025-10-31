namespace ItemsShop.Catalogs.Features.Shared.Consts;

internal static class CategoriesConsts
{
    internal const string BaseRoute = "/api/v1/categories";

    internal const string GetCategory = BaseRoute + "/{id:guid}";

    internal const string DeleteCategory = BaseRoute + "/{id:guid}";

    internal const string UpdateCategoryName = BaseRoute + "/{id:guid}/name";

    internal const string UpdateCategoryDescription = BaseRoute + "/{id:guid}/description";
}

internal static class CategoriesTagConsts
{
    internal static readonly string[] CategoriesEndpointTags = ["Categories"];
}
