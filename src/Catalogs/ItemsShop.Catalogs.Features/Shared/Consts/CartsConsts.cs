namespace ItemsShop.Catalogs.Features.Shared.Consts;

internal static class CartsRouteConsts
{
    internal const string BaseRoute = "/api/v1/carts";

    internal const string GetCart = BaseRoute + "/{cartId:guid}";

    internal const string DeleteCart = BaseRoute + "/{cartId:guid}";
}

internal static class CartsTagConsts
{
    internal static readonly string[] CartsEndpointTags = ["Carts"];
}
