using Catalog.Domain.DTOs;
using Catalog.Infrastructure.Context;
using Domain.Common.Enums;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesQueryResponse>
{
    private readonly CatalogDbContext _context;

    public GetCategoriesQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetCategoriesQueryResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories
            .AsQueryable()
            .AsNoTracking();

        if (request.Name is not null)
        {
            query = query
                .Where(x => x.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
        }

        if (request.OrderType is not null)
        {
            query = request.OrderType == OrderQueryType.Ascending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name);
        }

        var categories = await query
            .Select(x => new CategoryDto(x.Id, x.Name, x.Description))
            .ToListAsync(cancellationToken);

        return new GetCategoriesQueryResponse(categories);
    }
}
