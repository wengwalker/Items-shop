namespace Catalog.Domain.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public long StockQuantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CategoryId { get; set; }

    public CategoryEntity Category { get; set; } = null!;
}
