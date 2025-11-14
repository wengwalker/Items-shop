using ItemsShop.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemsShop.Orders.Infrastructure.Database.Mapping;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .ToTable("Orders")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Status)
            .HasDefaultValue(0);

        builder
            .Property(x => x.TotalPrice)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnType("timestamp with time zone");

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnType("timestamp with time zone");
    }
}
