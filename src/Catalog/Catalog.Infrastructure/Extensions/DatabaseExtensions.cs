using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static IApplicationBuilder MigrateDatabase<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<T>();

        context.Database.Migrate();

        return app;
    }
}
