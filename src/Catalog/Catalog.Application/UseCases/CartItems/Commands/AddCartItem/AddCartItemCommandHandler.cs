using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.CartItems.Commands.AddCartItem;

public sealed class AddCartItemCommandHandler : IRequestHandler<AddCartItemCommand, AddCartItemCommandResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<AddCartItemCommand> _validator;

    public AddCartItemCommandHandler(CatalogDbContext context, IValidator<AddCartItemCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<AddCartItemCommandResponse> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        bool cartExists = await _context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            throw new NotFoundException(nameof(CartEntity));
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(ProductEntity));

        if (product.StockQuantity < request.Quantity)
        {
            throw new NotEnoughProductQuantityException($"Required quantity ({request.Quantity}) exceeds the quantity of the "
                + $" product ({product.Id}) of {product.StockQuantity} items");
        }

        var cartItem = new CartItemEntity
        {
            Id = Guid.NewGuid(),
            Quantity = request.Quantity,
            CartId = request.CartId,
            ProductId = request.ProductId
        };

        _context.CartItems.Add(cartItem);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddCartItemCommandResponse(
            cartItem.Id,
            cartItem.Quantity,
            cartItem.CartId,
            cartItem.ProductId);
    }
}
