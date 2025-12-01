using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

internal interface IUpdateCategoryDescriptionHandler : IHandler
{
    Task<Result<CategoryResponse>> HandleAsync(UpdateCategoryDescriptionRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateCategoryDescriptionHandler(
    CatalogDbContext context,
    ILogger<UpdateCategoryDescriptionHandler> logger)
    : IUpdateCategoryDescriptionHandler
{
    public async Task<Result<CategoryResponse>> HandleAsync(UpdateCategoryDescriptionRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating category with Id: {CategoryId}, to new description", request.CategoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<CategoryResponse>.Failure($"Category with ID {request.CategoryId} does not exists", ErrorType.NotFound);
        }

        category.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated category with Id: {CategoryId}, to new description", request.CategoryId);

        return Result<CategoryResponse>.Success(category.MapToResponse());
    }
}
