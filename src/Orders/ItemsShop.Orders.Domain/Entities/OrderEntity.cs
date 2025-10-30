using ItemsShop.Orders.Domain.Enums;

namespace ItemsShop.Orders.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; } = [];
}
