using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductCommandHandler(CatalogDbContext context, IValidator<UpdateProductCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ProductEntity));

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.CategoryId);
    }
}
