namespace ItemsShop.Orders.Domain.Entities;

public class OrderItemEntity
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public decimal ProductPrice { get; set; }

    public long ProductQuantity { get; set; }

    public decimal ItemPrice { get; set; }

    public Guid OrderId { get; set; }

    public OrderEntity Order { get; set; } = null!;
}
