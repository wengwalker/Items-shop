using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Common.Infrastructure.Abstractions;

public interface IDatabaseMigrator
{
    Task MigrateAsync(IServiceScope scope, CancellationToken cancellationToken = default);
}
