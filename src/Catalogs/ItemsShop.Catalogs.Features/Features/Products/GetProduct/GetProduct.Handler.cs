using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

internal interface IGetProductHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(GetProductRequest request, CancellationToken cancellationToken);
}

internal sealed class GetProductHandler(
    CatalogDbContext context,
    ILogger<GetProductHandler> logger)
    : IGetProductHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching product with Id: {ProductId}", request.ProductId);

        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id: {ProductId} does not exists", request.ProductId);

            return Result<ProductResponse>.Failure($"Product with ID {request.ProductId} does not exists", ErrorType.NotFound);
        }

        logger.LogInformation("Fetched product with Id: {ProductId}", request.ProductId);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
