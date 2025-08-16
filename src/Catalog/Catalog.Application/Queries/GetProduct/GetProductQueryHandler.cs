using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Queries.GetProduct;

public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<GetProductQuery> _validator;

    public GetProductQueryHandler(CatalogDbContext context, IValidator<GetProductQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(ProductEntity));

        return new GetProductQueryResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.CategoryId);
    }
}
