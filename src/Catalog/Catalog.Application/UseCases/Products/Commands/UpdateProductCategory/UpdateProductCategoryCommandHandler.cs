using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Products.Commands.UpdateProductCategory;

public sealed class UpdateProductCategoryCommandHandler(CatalogDbContext context, IValidator<UpdateProductCategoryCommand> validator) : IRequestHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<UpdateProductCategoryCommand> _validator = validator;

    public async Task<UpdateProductCategoryCommandResponse> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var categoryExists = await _context.Categories
            .AnyAsync(x => x.Id == request.NewCategoryId, cancellationToken);

        if (!categoryExists)
        {
            throw new NotFoundException(nameof(CategoryEntity));
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken)
            ?? throw new NotFoundException(nameof(ProductEntity));

        product.CategoryId = request.NewCategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateProductCategoryCommandResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.StockQuantity,
            product.CreatedAt,
            product.CategoryId);
    }
}
