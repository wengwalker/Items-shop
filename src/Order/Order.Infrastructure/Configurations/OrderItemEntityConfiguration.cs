using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Configurations;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder
            .ToTable("OrderItems")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.ProductItemId)
            .IsRequired();

        builder
            .Property(x => x.ProductItemPrice)
            .IsRequired();

        builder
            .Property(x => x.ItemsQuantity)
            .IsRequired();

        builder
            .Property(x => x.ItemsPrice)
            .IsRequired();
    }
}
