using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalog.Infrastructure.Database;

public class CatalogDatabaseMigrator : IDatabaseMigrator
{
    public async Task MigrateAsync(IServiceScope scope, CancellationToken cancellationToken = default)
    {
        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}
