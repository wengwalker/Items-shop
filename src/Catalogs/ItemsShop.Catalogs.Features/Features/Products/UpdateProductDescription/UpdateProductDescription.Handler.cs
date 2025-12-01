using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductDescription;

internal interface IUpdateProductDescriptionHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(UpdateProductDescriptionRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateProductDescriptionHandler(
    CatalogDbContext context,
    ILogger<UpdateProductDescriptionHandler> logger)
    : IUpdateProductDescriptionHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductDescriptionRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new description", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<ProductResponse>.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        product.Description = request.Description;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new description", request.ProductId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
