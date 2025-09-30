using Order.Domain.Enums;

namespace Order.Domain.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Price { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; } = [];
}
