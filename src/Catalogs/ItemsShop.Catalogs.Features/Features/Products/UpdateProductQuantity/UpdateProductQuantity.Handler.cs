using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

internal interface IUpdateProductQuantityHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(UpdateProductQuantityRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateProductQuantityHandler(
    CatalogDbContext context,
    ILogger<UpdateProductQuantityHandler> logger)
    : IUpdateProductQuantityHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductQuantityRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new stock quantity", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<ProductResponse>.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        product.Quantity = request.Quantity;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new stock quantity", request.ProductId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
