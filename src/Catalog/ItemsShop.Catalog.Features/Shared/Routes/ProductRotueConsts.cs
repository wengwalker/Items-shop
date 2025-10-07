namespace ItemsShop.Catalog.Features.Shared.Routes;

internal static class ProductRotueConsts
{
    internal const string BaseRoute = "/api/v1/products";

    internal const string DeleteProduct = BaseRoute + "/{id}";

    internal const string UpdateProductCategory = BaseRoute + "/{id}/category";

    internal const string UpdateProductDescription = BaseRoute + "/{id}/description";

    internal const string UpdateProductPrice = BaseRoute + "/{id}/price";

    internal const string UpdateProductQuantity = BaseRoute + "/{id}/quantity";
}
