using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductPrice;

internal interface IUpdateProductPriceHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(UpdateProductPriceRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateProductPriceHandler(
    CatalogDbContext context,
    ILogger<UpdateProductPriceHandler> logger)
    : IUpdateProductPriceHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(UpdateProductPriceRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new price", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<ProductResponse>.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        product.Price = request.Price;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new price", request.ProductId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
