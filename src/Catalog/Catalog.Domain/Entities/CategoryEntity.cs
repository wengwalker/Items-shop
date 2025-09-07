namespace Catalog.Domain.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<ProductEntity> Products { get; set; } = [];
}
