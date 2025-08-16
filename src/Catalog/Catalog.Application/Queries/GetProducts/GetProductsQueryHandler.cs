using Catalog.Domain.DTOs;
using Catalog.Infrastructure.Context;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Queries.GetProducts;

public sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
{
    private readonly CatalogDbContext _context;

    public GetProductsQueryHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Select(x => new ProductDto(x.Id, x.Name, x.Description, x.Price, x.StockQuantity, x.CreatedAt, x.CategoryId))
            .ToListAsync(cancellationToken);

        return new GetProductsQueryResponse(products);
    }
}
