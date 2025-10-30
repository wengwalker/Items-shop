namespace ItemsShop.Orders.Features.Shared.Routes;

internal static class OrderItemsRouteConsts
{
    internal const string BaseRoute = "/api/v1/orders/{id:guid}/items";

    internal const string DeleteOrderItem = BaseRoute + "/{id:guid}";

    internal const string GetOrderItem = BaseRoute + "/{id:guid}";

    internal const string UpdateOrderItemQuantity = BaseRoute + "/{id:guid}";
}
