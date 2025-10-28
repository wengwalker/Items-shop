using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Common.Infrastructure.Abstractions;

public interface IModuleDatabaseMigrator
{
    Task MigrateAsync(IServiceScope scope, CancellationToken cancellationToken = default);
}
