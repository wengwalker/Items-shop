using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Commands.AddProduct;

public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductCommandResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<AddProductCommand> _validator;

    public AddProductCommandHandler(CatalogDbContext context, IValidator<AddProductCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<AddProductCommandResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken)
            ?? throw new NotFoundException(nameof(CategoryEntity));

        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = category.Id,
            Category = category
        };

        await _context.Products.AddAsync(product, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddProductCommandResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.CategoryId);
    }
}
