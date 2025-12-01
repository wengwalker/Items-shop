using ItemsShop.Catalogs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemsShop.Catalogs.Infrastructure.Database.Mapping;

public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder
            .ToTable("Products")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .HasDefaultValue(0);

        builder
            .Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
