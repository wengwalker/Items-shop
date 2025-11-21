using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.CreateCategory;

internal interface ICreateCategoryHandler : IHandler
{
    Task<Result<CategoryResponse>> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken);
}

internal sealed class CreateCategoryHandler(
    CatalogDbContext context,
    ILogger<CreateCategoryHandler> logger)
    : ICreateCategoryHandler
{
    public async Task<Result<CategoryResponse>> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating category with name: {Name}", request.Name);

        bool categoryExists = await context.Categories
            .AnyAsync(x => EF.Functions.ILike(x.Name, request.Name), cancellationToken);

        if (categoryExists)
        {
            logger.LogInformation("Category with Name {Name} already exists", request.Name);

            return Result<CategoryResponse>.Failure($"Category with Name {request.Name} already exists", ErrorType.Conflict);
        }

        var category = request.MapToCategory();

        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created category with Id: {Id}", category.Id);

        return Result<CategoryResponse>.Success(category.MapToResponse());
    }
}
