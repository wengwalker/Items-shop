using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Commands.DeleteCategory;

public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<DeleteCategoryCommand> _validator;

    public DeleteCategoryCommandHandler(CatalogDbContext context, IValidator<DeleteCategoryCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CategoryEntity));

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
