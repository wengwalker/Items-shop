using ItemsShop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemsShop.Catalog.Infrastructure.Database;

public class CatalogDbContext : DbContext
{
    public virtual DbSet<ProductEntity> Products { get; set; } = null!;

    public virtual DbSet<CategoryEntity> Categories { get; set; } = null!;

    public virtual DbSet<CartEntity> Carts { get; set; } = null!;

    public virtual DbSet<CartItemEntity> CartItems { get; set; } = null!;

    protected CatalogDbContext() { }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DbConsts.CatalogSchemaName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}
