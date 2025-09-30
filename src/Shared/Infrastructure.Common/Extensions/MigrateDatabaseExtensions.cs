using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions
{
    public static class MigrateDatabaseExtensions
    {
        public static async Task<IApplicationBuilder> MigrateDatabaseAsync<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<T>();

            await context.Database.MigrateAsync();

            return app;
        }
    }
}
