namespace Catalog.Domain.Entities;

public class CartItemEntity
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public CartEntity Cart { get; set; } = null!;

    public ProductEntity Product { get; set; } = null!;
}
