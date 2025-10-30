using ItemsShop.Orders.Domain.Entities;
using ItemsShop.Orders.Domain.Enums;
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
            .HasDefaultValue(OrderStatus.Draft);

        builder
            .Property(x => x.TotalPrice)
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValue(DateTime.UtcNow);

        builder
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
