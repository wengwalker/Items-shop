using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;
using Order.Domain.Enums;

namespace Order.Infrastructure.Configurations;

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
            .Property(x => x.Price)
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasDefaultValue(DateTime.UtcNow);

        builder
            .Property(x => x.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);

        builder
            .HasMany(x => x.OrderItems)
            .WithOne(x => x.Order)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
