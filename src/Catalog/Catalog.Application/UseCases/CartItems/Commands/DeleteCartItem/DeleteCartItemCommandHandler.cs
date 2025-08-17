using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.CartItems.Commands.DeleteCartItem;

public sealed class DeleteCartItemCommandHandler : IRequestHandler<DeleteCartItemCommand>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<DeleteCartItemCommand> _validator;

    public DeleteCartItemCommandHandler(CatalogDbContext context, IValidator<DeleteCartItemCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        bool cartExists = await _context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            throw new NotFoundException(nameof(CartEntity));
        }

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(CartItemEntity));

        _context.CartItems.Remove(cartItem);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
