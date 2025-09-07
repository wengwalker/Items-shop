using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Context;

public class CatalogDbContext : DbContext
{
    public virtual DbSet<ProductEntity> Products { get; set; }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<CartEntity> Carts { get; set; }

    public virtual DbSet<CartItemEntity> CartItems { get; set; }

    protected CatalogDbContext() { }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}
