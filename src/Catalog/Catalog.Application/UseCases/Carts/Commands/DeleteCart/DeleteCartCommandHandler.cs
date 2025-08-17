using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Carts.Commands.DeleteCart;

public sealed class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<DeleteCartCommand> _validator;

    public DeleteCartCommandHandler(CatalogDbContext context, IValidator<DeleteCartCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var cart = await _context.Carts
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CartEntity));

        _context.Carts.Remove(cart);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
