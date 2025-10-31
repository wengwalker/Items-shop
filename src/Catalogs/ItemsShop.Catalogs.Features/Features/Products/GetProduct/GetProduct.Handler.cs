using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IRequest<Result<GetProductResponse>>;

public sealed record GetProductResponse(ProductResponse Product);

public sealed class GetProductHandler(
    CatalogDbContext context,
    ILogger<GetProductHandler> logger) : IRequestHandler<GetProductQuery, Result<GetProductResponse>>
{
    public async Task<Result<GetProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching product with Id: {ProductId}", request.ProductId);

        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id: {ProductId} does not exists", request.ProductId);

            return Result<GetProductResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        GetProductResponse response = product.MapToResponse();

        logger.LogInformation("Fetched product with Id: {ProductId}", request.ProductId);

        return Result<GetProductResponse>.Success(response);
    }
}
