using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

internal interface IUpdateProductCategoryHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateProductCategoryHandler(
    CatalogDbContext context,
    ILogger<UpdateProductCategoryHandler> logger)
    : IUpdateProductCategoryHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductCategoryRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.productId, request.categoryId);

        var categoryExists = await context.Categories
            .AnyAsync(x => x.Id == request.categoryId, cancellationToken);

        if (!categoryExists)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.categoryId);

            return Result<ProductResponse>.Failure($"Category with ID {request.categoryId} does not exists", ErrorType.NotFound);
        }

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.productId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.productId);

            return Result<ProductResponse>.Failure($"Product with ID {request.productId} does not exists", ErrorType.NotFound);
        }

        product.CategoryId = request.categoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.productId, request.categoryId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
