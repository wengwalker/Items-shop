using Catalog.Domain.DTOs;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.CartItems.Queries.GetCartItems;

public sealed class GetCartItemsQueryHandler : IRequestHandler<GetCartItemsQuery, GetCartItemsQueryResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<GetCartItemsQuery> _validator;

    public GetCartItemsQueryHandler(CatalogDbContext context, IValidator<GetCartItemsQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<GetCartItemsQueryResponse> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cart = await _context.Carts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken)
            ?? throw new NotFoundException(nameof(CartEntity));

        var cartItems = await _context.CartItems
            .AsNoTracking()
            .Where(x => x.CartId == request.CartId)
            .Select(x => new CartItemDto(x.Id, x.Quantity, x.CartId, x.ProductId))
            .ToListAsync(cancellationToken);

        return new GetCartItemsQueryResponse(cartItems);
    }
}
