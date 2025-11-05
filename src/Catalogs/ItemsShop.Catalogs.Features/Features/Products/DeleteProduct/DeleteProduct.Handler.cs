using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.DeleteProduct;

public sealed record DeleteProductCommand(
    Guid ProductId) : IRequest<Result<DeleteProductResponse>>;

public sealed record DeleteProductResponse();

public sealed class DeleteProductHandler(
    CatalogDbContext context,
    ILogger<DeleteProductHandler> logger) : IRequestHandler<DeleteProductCommand, Result<DeleteProductResponse>>
{
    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with ID: {ProductId}", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with ID {ProductId} does not exists", request.ProductId);

            return Result<DeleteProductResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted product with ID {ProductId}", product.Id);

        return Result<DeleteProductResponse>.Success();
    }
}
