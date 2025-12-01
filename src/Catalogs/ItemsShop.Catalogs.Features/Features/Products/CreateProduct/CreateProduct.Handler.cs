using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.CreateProduct;

internal interface ICreateProductHandler : IHandler
{
    Task<Result<ProductResponse>> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken);
}

internal sealed class CreateProductHandler(
    CatalogDbContext context,
    ILogger<CreateProductHandler> logger)
    : ICreateProductHandler
{
    public async Task<Result<ProductResponse>> HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating product with name: {Name}", request.Name);

        bool categoryExists = await context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<ProductResponse>
                .Failure($"Category with ID {request.CategoryId} does not exists", ErrorType.NotFound);
        }

        var product = request.MapToProduct();

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created product with Id {ProductId}", product.Id);

        return Result<ProductResponse>.Success(product.MapToResponse());
    }
}
