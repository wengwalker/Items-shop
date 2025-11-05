using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.DeleteCategory;

public sealed record DeleteCategoryCommand(
    Guid CategoryId) : IRequest<Result<DeleteCategoryResponse>>;

public sealed record DeleteCategoryResponse();

public sealed class DeleteCategoryHandler(
    CatalogDbContext context,
    ILogger<DeleteCategoryHandler> logger) : IRequestHandler<DeleteCategoryCommand, Result<DeleteCategoryResponse>>
{
    public async Task<Result<DeleteCategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting category with Id: {CategoryId}", request.CategoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<DeleteCategoryResponse>
                .Failure($"Category with Id {request.CategoryId} does not exists", StatusCodes.Status404NotFound);
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted category with Id: {CategoryId}", category.Id);

        return Result<DeleteCategoryResponse>.Success();
    }
}
