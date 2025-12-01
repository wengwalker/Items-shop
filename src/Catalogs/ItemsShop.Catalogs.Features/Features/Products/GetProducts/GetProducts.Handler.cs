using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProducts;

internal interface IGetProductsHandler : IHandler
{
    Task<Result<List<ProductResponse>>> HandleAsync(GetProductsRequest request, CancellationToken cancellationToken);
}

internal sealed class GetProductsHandler(
    CatalogDbContext context,
    ILogger<GetProductsHandler> logger)
    : IGetProductsHandler
{
    public async Task<Result<List<ProductResponse>>> HandleAsync(GetProductsRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching products");

        var query = context.Products
            .AsQueryable()
            .AsNoTracking();

        if (request.Name is not null)
        {
            logger.LogInformation("Fetching products with specified name like {Name}", request.Name);

            query = query.Where(x => EF.Functions.ILike(x.Name, $"%{request.Name}%"));
        }

        if (request.SortType is not null)
        {
            logger.LogInformation("Fetching products in specified order: {OrderType}", request.SortType);

            query = request.SortType == QuerySortType.Ascending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name);
        }

        List<ProductResponse> products = await query
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        logger.LogInformation("Fetched {Count} products", products.Count);

        return Result<List<ProductResponse>>.Success(products);
    }
}
