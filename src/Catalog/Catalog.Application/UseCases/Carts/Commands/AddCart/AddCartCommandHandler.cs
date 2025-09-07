using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Carts.Commands.AddCart;

public sealed class AddCartCommandHandler : IRequestHandler<AddCartCommand, AddCartCommandResponse>
{
    private readonly CatalogDbContext _context;

    public AddCartCommandHandler(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<AddCartCommandResponse> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new CartEntity
        {
            Id = Guid.NewGuid()
        };

        _context.Carts.Add(cart);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddCartCommandResponse(cart.Id, cart.LastUpdated);
    }
}
