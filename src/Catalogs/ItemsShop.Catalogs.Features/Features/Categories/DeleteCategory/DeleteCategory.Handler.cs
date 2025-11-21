using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.DeleteCategory;

internal interface IDeleteCategoryHandler : IHandler
{
    Task<Result> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteCategoryHandler(
    CatalogDbContext context,
    ILogger<DeleteCategoryHandler> logger)
    : IDeleteCategoryHandler
{
    public async Task<Result> HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting category with Id: {CategoryId}", request.categoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.categoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.categoryId);

            return Result.Failure($"Category with Id {request.categoryId} does not exists", ErrorType.NotFound);
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted category with Id: {CategoryId}", category.Id);

        return Result.Success();
    }
}
