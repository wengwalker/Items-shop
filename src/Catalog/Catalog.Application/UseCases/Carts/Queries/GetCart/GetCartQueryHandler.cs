using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Carts.Queries.GetCart;

public sealed class GetCartQueryHandler(CatalogDbContext context, IValidator<GetCartQuery> validator) : IRequestHandler<GetCartQuery, GetCartQueryResponse>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<GetCartQuery> _validator = validator;

    public async Task<GetCartQueryResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cart = await _context.Carts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CartEntity));

        return new GetCartQueryResponse(
            cart.Id,
            cart.LastUpdated);
    }
}
