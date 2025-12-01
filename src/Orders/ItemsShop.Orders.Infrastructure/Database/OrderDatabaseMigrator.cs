using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Orders.Infrastructure.Database;

public class OrderDatabaseMigrator : IModuleDatabaseMigrator
{
    public async Task MigrateAsync(IServiceScope scope, CancellationToken cancellationToken = default)
    {
        var context = scope.ServiceProvider.GetRequiredService<OrderDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}
