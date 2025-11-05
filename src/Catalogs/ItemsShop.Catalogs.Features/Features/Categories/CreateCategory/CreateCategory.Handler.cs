using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name,
    string? Description) : IRequest<Result<CreateCategoryResponse>>;

public sealed record CreateCategoryResponse(
    Guid CategoryId,
    string Name,
    string? Description);

public sealed class CreateCategoryHandler(
    CatalogDbContext context,
    ILogger<CreateCategoryHandler> logger) : IRequestHandler<CreateCategoryCommand, Result<CreateCategoryResponse>>
{
    public async Task<Result<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating category with name: {Name}", request.Name);

        bool categoryExists = await context.Categories
            .AnyAsync(x => EF.Functions.ILike(x.Name, request.Name), cancellationToken);

        if (categoryExists)
        {
            logger.LogInformation("Category with Name {Name} already exists", request.Name);

            return Result<CreateCategoryResponse>
                .Failure($"Category with Name {request.Name} already exists", StatusCodes.Status409Conflict);
        }

        var category = request.MapToCategory();

        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created category with Id: {Id}", category.Id);

        var response = category.MapToResponse();

        return Result<CreateCategoryResponse>.Success(response);
    }
}
