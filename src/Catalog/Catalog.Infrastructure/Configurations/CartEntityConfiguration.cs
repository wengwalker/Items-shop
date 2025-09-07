using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Configurations;

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
