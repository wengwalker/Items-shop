using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Common.Infrastructure.Extensions;

public static class DatabaseMigrationExtensions
{
    public static async Task MigrateDatabasesAsync(this IServiceScope scope, CancellationToken cancellationToken = default)
    {
        var migrators = scope.ServiceProvider.GetRequiredService<IEnumerable<IModuleDatabaseMigrator>>();

        foreach (var migrator in migrators)
        {
            await migrator.MigrateAsync(scope, cancellationToken);
        }
    }
}
