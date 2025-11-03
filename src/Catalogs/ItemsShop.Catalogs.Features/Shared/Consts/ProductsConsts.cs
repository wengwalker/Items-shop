namespace ItemsShop.Catalogs.Features.Shared.Consts;

internal static class ProductsRouteConsts
{
    internal const string BaseRoute = "/api/v1/products";

    internal const string GetProduct = BaseRoute + "/{productId:guid}";

    internal const string DeleteProduct = BaseRoute + "/{productId:guid}";

    internal const string UpdateProductCategory = BaseRoute + "/{productId:guid}/category";

    internal const string UpdateProductDescription = BaseRoute + "/{productId:guid}/description";

    internal const string UpdateProductPrice = BaseRoute + "/{productId:guid}/price";

    internal const string UpdateProductQuantity = BaseRoute + "/{productId:guid}/quantity";
}

internal static class ProductsTagConsts
{
    internal static readonly string[] ProductsEndpointTags = ["Products"];
}
