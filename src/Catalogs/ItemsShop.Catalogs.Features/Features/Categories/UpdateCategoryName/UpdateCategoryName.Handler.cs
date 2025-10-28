using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

public sealed record UpdateCategoryNameCommand(
    Guid CategoryId,
    string Name) : IRequest<Result<UpdateCategoryNameResponse>>;

public sealed record UpdateCategoryNameResponse(
    Guid CategoryId,
    string Name);

public sealed class UpdateCategoryNameHandler(
    CatalogDbContext context,
    ILogger<UpdateCategoryNameHandler> logger) : IRequestHandler<UpdateCategoryNameCommand, Result<UpdateCategoryNameResponse>>
{
    public async Task<Result<UpdateCategoryNameResponse>> Handle(UpdateCategoryNameCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating category with Id: {CategoryId}, to new name", request.CategoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<UpdateCategoryNameResponse>
                .Failure($"Category with ID {request.CategoryId} does not exists", StatusCodes.Status404NotFound);
        }

        category.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated category with Id: {CategoryId}, to new name", request.CategoryId);

        var response = category.MapToResponse();

        return Result<UpdateCategoryNameResponse>.Success(response);
    }
}
