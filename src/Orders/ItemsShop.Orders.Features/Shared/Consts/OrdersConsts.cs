namespace ItemsShop.Orders.Features.Shared.Consts;

internal static class OrdersRouteConsts
{
    internal const string BaseRoute = "/api/v1/orders";

    internal const string DeleteOrder = BaseRoute + "/{id:guid}";

    internal const string UpdateOrderPrice = BaseRoute + "/{id:guid}";
}

internal static class OrdersTagConsts
{
    internal static readonly string[] OrdersEndpointTags = ["Orders"];
}
