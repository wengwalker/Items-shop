namespace ItemsShop.Orders.Features.Shared.Consts;

internal static class OrderItemsRouteConsts
{
    internal const string BaseRoute = "/api/v1/orders/{orderId:guid}/items";

    internal const string DeleteOrderItem = BaseRoute + "/{itemId:guid}";

    internal const string GetOrderItem = BaseRoute + "/{itemId:guid}";

    internal const string UpdateOrderItemQuantity = BaseRoute + "/{itemId:guid}";
}

internal static class OrderItemsTagConsts
{
    internal static readonly string[] OrderItemsEndpointTags = ["OrderItems"];
}
