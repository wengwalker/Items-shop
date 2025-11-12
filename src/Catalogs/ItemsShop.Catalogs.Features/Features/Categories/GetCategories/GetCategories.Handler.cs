using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Application.Enums;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

public sealed record GetCategoriesQuery(
    string? Name,
    QuerySortType? SortType) : IRequest<Result<GetCategoriesResponse>>;

public sealed record GetCategoriesResponse(
    ICollection<CategoryResponse> Categories);

public sealed class GetCategoriesHandler(
    CatalogDbContext context,
    ILogger<GetCategoriesHandler> logger) : IRequestHandler<GetCategoriesQuery, Result<GetCategoriesResponse>>
{
    public async Task<Result<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching categories");

        var query = context.Categories
            .AsQueryable()
            .AsNoTracking();

        if (request.Name is not null)
        {
            logger.LogInformation("Fetching categories with specified name like {Name}", request.Name);

            query = query.Where(x => EF.Functions.ILike(x.Name, $"%{request.Name}%"));
        }

        if (request.SortType is not null)
        {
            logger.LogInformation("Fetching categories in specified order: {OrderType}", request.SortType);

            query = request.SortType == QuerySortType.Ascending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name);
        }

        List<CategoryResponse> categories = await query
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        var response = categories.MapToResponse();

        logger.LogInformation("Fetched {Count} categories", response.Categories.Count);

        return Result<GetCategoriesResponse>.Success(response);
    }
}
