using Catalog.Domain.DTOs;
using Catalog.Infrastructure.Context;
using Domain.Common.Enums;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Products.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
{
    private readonly CatalogDbContext _context;

    public GetProductsQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products
            .AsQueryable()
            .AsNoTracking();

        if (request.Name is not null)
        {
            query = query
                .Where(x => EF.Functions.ILike(x.Name, $"%{request.Name}%"));
        }

        if (request.OrderType is not null)
        {
            query = request.OrderType == OrderQueryType.Ascending
                ? query.OrderBy(x => x.Name)
                : query.OrderByDescending(x => x.Name);
        }

        var products = await query
            .Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, x.StockQuantity, x.CreatedAt, x.CategoryId))
            .ToListAsync(cancellationToken);

        return new GetProductsQueryResponse(products);
    }
}
