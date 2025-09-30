using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Context;

public class OrderDbContext : DbContext
{
    public virtual DbSet<OrderEntity> Orders { get; set; } = null!;

    public virtual DbSet<OrderItemEntity> OrderItems { get; set; } = null!;

    protected OrderDbContext() { }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
