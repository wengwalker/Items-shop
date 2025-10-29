using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Catalogs.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(CatalogDbContext)), npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName));
        });

        services.AddScoped<IModuleDatabaseMigrator, CatalogDatabaseMigrator>();

        return services;
    }
}
