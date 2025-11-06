using ItemsShop.Common.Application.Extensions;
using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Features.Shared.Responses;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

internal static class GetOrdersMappingExtensions
{
    public static GetOrdersQuery MapToQuery(this GetOrdersRequest request)
        => new(request.sortType,
                request.status,
                request.biggerOrEqualPrice,
                request.lessOrEqualPrice,
                request.createdBefore,
                request.createdAfter,
                request.updatedBefore,
                request.updatedAfter);

    public static OrderResponse MapToResponse(this OrderEntity order)
        => new(order.Id,
                order.Status.MapValueToOrderStatus(),
                order.TotalPrice,
                order.CreatedAt,
                order.UpdatedAt);

    public static GetOrdersResponse MapToResponse(this ICollection<OrderResponse> responses)
        => new(responses);
}
