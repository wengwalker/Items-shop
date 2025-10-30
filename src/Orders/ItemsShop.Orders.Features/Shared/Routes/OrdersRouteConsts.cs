namespace ItemsShop.Orders.Features.Shared.Routes;

internal static class OrdersRouteConsts
{
    internal const string BaseRoute = "/api/v1/orders";

    internal const string DeleteOrder = BaseRoute + "/{id:guid}";

    internal const string UpdateOrderPrice = BaseRoute + "/{id:guid}";
}
