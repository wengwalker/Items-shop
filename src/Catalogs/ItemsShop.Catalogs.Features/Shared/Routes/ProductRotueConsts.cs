namespace ItemsShop.Catalogs.Features.Shared.Routes;

internal static class ProductRouteConsts
{
    internal const string BaseRoute = "/api/v1/products";

    internal const string GetProduct = BaseRoute + "/{id:guid}";

    internal const string DeleteProduct = BaseRoute + "/{id:guid}";

    internal const string UpdateProductCategory = BaseRoute + "/{id:guid}/category";

    internal const string UpdateProductDescription = BaseRoute + "/{id:guid}/description";

    internal const string UpdateProductPrice = BaseRoute + "/{id:guid}/price";

    internal const string UpdateProductQuantity = BaseRoute + "/{id:guid}/quantity";
}
