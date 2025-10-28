namespace ItemsShop.Catalogs.Domain.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public long Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryEntity Category { get; set; } = null!;
}
