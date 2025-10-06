using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString(nameof(CatalogDbContext));

        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(CatalogDbContext)), npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName));
        });

        services.AddScoped<IDatabaseMigrator, CatalogDatabaseMigrator>();

        return services;
    }
}
