using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Products.Commands.AddProduct;

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

        bool categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            throw new NotFoundException(nameof(CategoryEntity));
        }

        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(product);

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
