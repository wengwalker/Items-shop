namespace ItemsShop.Catalog.Features.Shared.Routes;

internal static class CartsRouteConsts
{
    internal const string BaseRoute = "/api/v1/carts";

    internal const string GetCart = BaseRoute + "/{id:guid}";

    internal const string DeleteCart = BaseRoute + "/{id:guid}";
}
