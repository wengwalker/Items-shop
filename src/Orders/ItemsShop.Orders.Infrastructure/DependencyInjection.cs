using ItemsShop.Common.Infrastructure.Abstractions;
using ItemsShop.Orders.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItemsShop.Orders.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOrderInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(OrderDbContext)), npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName));
        });

        services.AddScoped<IModuleDatabaseMigrator, OrderDatabaseMigrator>();

        return services;
    }
}
