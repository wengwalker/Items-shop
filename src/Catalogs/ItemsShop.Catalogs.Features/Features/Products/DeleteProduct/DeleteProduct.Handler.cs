using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.DeleteProduct;

internal interface IDeleteProductHandler : IHandler
{
    Task<Result> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteProductHandler(
    CatalogDbContext context,
    ILogger<DeleteProductHandler> logger)
    : IDeleteProductHandler
{
    public async Task<Result> HandleAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with ID: {ProductId}", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with ID {ProductId} does not exists", request.ProductId);

            return Result.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted product with ID {ProductId}", product.Id);

        return Result.Success();
    }
}
