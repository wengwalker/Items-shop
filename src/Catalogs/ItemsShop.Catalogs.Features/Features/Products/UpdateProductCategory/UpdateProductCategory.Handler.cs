using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

public sealed record UpdateProductCategoryCommand(
    Guid ProductId,
    Guid CategoryId) : IRequest<Result<UpdateProductCategoryResponse>>;

public sealed record UpdateProductCategoryResponse(
    Guid ProductId,
    Guid CategoryId);

public sealed class UpdateProductCategoryHandler(
    CatalogDbContext context,
    ILogger<UpdateProductCategoryHandler> logger) : IRequestHandler<UpdateProductCategoryCommand, Result<UpdateProductCategoryResponse>>
{
    public async Task<Result<UpdateProductCategoryResponse>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.ProductId, request.CategoryId);

        var categoryExists = await context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<UpdateProductCategoryResponse>
                .Failure($"Category with ID {request.CategoryId} does not exists", StatusCodes.Status404NotFound);
        }

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<UpdateProductCategoryResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        product.CategoryId = request.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.ProductId, request.CategoryId);

        var response = product.MapToResponse();

        return Result<UpdateProductCategoryResponse>.Success(response);
    }
}
