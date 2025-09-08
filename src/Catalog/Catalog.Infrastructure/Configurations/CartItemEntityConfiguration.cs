using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations;

public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
{
    public void Configure(EntityTypeBuilder<CartItemEntity> builder)
    {
        builder
            .ToTable("CartItems")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Quantity)
            .HasDefaultValue(1)
            .IsRequired();

        builder
            .HasOne(x => x.Cart)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(x => new { x.CartId, x.ProductId })
            .IsUnique();
    }
}
