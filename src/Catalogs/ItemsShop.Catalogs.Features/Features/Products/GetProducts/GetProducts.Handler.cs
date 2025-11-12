using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProducts;

public sealed record GetProductsQuery(
    string? Name,
    QuerySortType? SortType) : IRequest<Result<GetProductsResponse>>;

public sealed record GetProductsResponse(ICollection<ProductResponse> Products);

public sealed class GetProductsHandler(
    CatalogDbContext context,
    ILogger<GetProductsHandler> logger)
    : IRequestHandler<GetProductsQuery, Result<GetProductsResponse>>
{
    public async Task<Result<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
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

        var response = products.MapToResponse();

        logger.LogInformation("Fetched {Count} products", response.Products.Count);

        return Result<GetProductsResponse>.Success(response);
    }
}
