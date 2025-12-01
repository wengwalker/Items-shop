using ItemsShop.Common.Application.Enums;

namespace ItemsShop.Common.Application.Extensions;

public static class OrderStatusExtensions
{
    public static OrderStatus MapValueToOrderStatus(this byte value)
    {
        if (value >= (byte)OrderStatus.Draft && value <= (byte)OrderStatus.Finished)
        {
            return (OrderStatus)value;
        }

        throw new ArgumentOutOfRangeException(nameof(OrderStatus));
    }
}
