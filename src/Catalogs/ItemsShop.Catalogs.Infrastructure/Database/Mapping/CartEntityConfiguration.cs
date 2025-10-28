using ItemsShop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemsShop.Catalog.Infrastructure.Database.Mapping;

public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
{
    public void Configure(EntityTypeBuilder<CartEntity> builder)
    {
        builder
            .ToTable("Carts")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.LastUpdated)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValue(DateTime.UtcNow);
    }
}
