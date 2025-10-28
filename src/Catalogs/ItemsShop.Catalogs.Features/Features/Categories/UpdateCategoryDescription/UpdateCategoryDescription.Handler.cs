using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

public sealed record UpdateCategoryDescriptionCommand(
    Guid CategoryId,
    string? Description) : IRequest<Result<UpdateCategoryDescriptionResponse>>;

public sealed record UpdateCategoryDescriptionResponse(
    Guid CategoryId,
    string? Description);

public sealed class UpdateCategoryDescriptionHandler(
    CatalogDbContext context,
    ILogger<UpdateCategoryDescriptionHandler> logger) : IRequestHandler<UpdateCategoryDescriptionCommand, Result<UpdateCategoryDescriptionResponse>>
{
    public async Task<Result<UpdateCategoryDescriptionResponse>> Handle(UpdateCategoryDescriptionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating category with Id: {CategoryId}, to new description", request.CategoryId);

        var category = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category == null)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<UpdateCategoryDescriptionResponse>
                .Failure($"Category with ID {request.CategoryId} does not exists", StatusCodes.Status404NotFound);
        }

        category.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated category with Id: {CategoryId}, to new description", request.CategoryId);

        var response = category.MapToResponse();

        return Result<UpdateCategoryDescriptionResponse>.Success(response);
    }
}
