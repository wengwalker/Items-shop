using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

internal interface IUpdateCategoryNameHandler : IHandler
{
    Task<Result<CategoryResponse>> HandleAsync(UpdateCategoryNameRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateCategoryNameHandler(
    CatalogDbContext context,
    ILogger<UpdateCategoryNameHandler> logger)
    : IUpdateCategoryNameHandler
{
    public async Task<Result<CategoryResponse>> HandleAsync(UpdateCategoryNameRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating category with Id: {CategoryId}, to new name", request.categoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.categoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.categoryId);

            return Result<CategoryResponse>.Failure($"Category with ID {request.categoryId} does not exists", ErrorType.NotFound);
        }

        category.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated category with Id: {CategoryId}, to new name", request.categoryId);

        return Result<CategoryResponse>.Success(category.MapToResponse());
    }
}
