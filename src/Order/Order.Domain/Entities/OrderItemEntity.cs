namespace Order.Domain.Entities;

public class OrderItemEntity
{
    public Guid Id { get; set; }

    public Guid ProductItemId { get; set; }

    public decimal ProductItemPrice { get; set; }

    public decimal ItemsQuantity { get; set; }

    public decimal ItemsPrice { get; set; }

    public OrderEntity Order { get; set; } = null!;
}
