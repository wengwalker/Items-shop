using ItemsShop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemsShop.Catalog.Infrastructure.Database.Mapping;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder
            .ToTable("Categories")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(70)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(150);

        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}
