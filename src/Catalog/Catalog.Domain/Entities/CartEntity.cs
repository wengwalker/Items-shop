namespace Catalog.Domain.Entities;

public class CartEntity
{
    public Guid Id { get; set; }

    public DateTime LastUpdated { get; set; }

    public List<CartItemEntity> Items { get; set; } = [];
}
