using ItemsShop.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemsShop.Orders.Infrastructure.Database;

public class OrderDbContext : DbContext
{
    public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

    public virtual DbSet<OrderItemEntity> OrderItems { get; set; } = null!;

    protected OrderDbContext() { }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DbConsts.OrderSchemaName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
