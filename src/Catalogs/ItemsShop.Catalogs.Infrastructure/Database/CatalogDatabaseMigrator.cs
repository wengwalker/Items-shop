using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalogs.Infrastructure.Database;

public class CatalogDatabaseMigrator : IModuleDatabaseMigrator
{
    public async Task MigrateAsync(IServiceScope scope, CancellationToken cancellationToken = default)
    {
        var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}
