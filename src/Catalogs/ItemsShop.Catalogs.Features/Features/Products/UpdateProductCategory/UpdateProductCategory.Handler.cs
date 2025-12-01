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
        logger.LogInformation("Updating product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.ProductId, request.CategoryId);

        var categoryExists = await context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<ProductResponse>.Failure($"Category with ID {request.CategoryId} does not exists", ErrorType.NotFound);
        }

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<ProductResponse>.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        product.CategoryId = request.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new category with Id: {NewCategoryId}", request.ProductId, request.CategoryId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
