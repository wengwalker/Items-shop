namespace ItemsShop.Catalogs.Features.Shared.Routes;

internal static class CartItemsRouteConsts
{
    internal const string BaseRoute = "/api/v1/carts/{cartId:guid}/items";

    internal const string GetCartItem = BaseRoute + "/{itemId:guid}";

    internal const string DeleteCartItem = BaseRoute + "/{itemId:guid}";
}
