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
        var postgresConnectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(postgresConnectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName));
        });

        services.AddScoped<IModuleDatabaseMigrator, CatalogDatabaseMigrator>();

        return services;
    }
}
