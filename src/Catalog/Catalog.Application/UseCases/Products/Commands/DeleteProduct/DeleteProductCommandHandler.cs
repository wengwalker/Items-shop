using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler(CatalogDbContext context, IValidator<DeleteProductCommand> validator) : IRequestHandler<DeleteProductCommand>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<DeleteProductCommand> _validator = validator;

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(ProductEntity));

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
