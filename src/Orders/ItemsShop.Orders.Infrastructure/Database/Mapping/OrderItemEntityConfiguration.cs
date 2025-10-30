using ItemsShop.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemsShop.Orders.Infrastructure.Database.Mapping;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder
            .ToTable("OrderItems")
            .HasKey(x => x.Id);

        builder
            .Property(x => x.ProductId)
            .IsRequired();

        builder
            .Property(x => x.ItemPrice)
            .IsRequired();

        builder
            .Property(x => x.ItemPrice)
            .IsRequired();

        builder
            .Property(x => x.ProductQuantity)
            .IsRequired();
    }
}
