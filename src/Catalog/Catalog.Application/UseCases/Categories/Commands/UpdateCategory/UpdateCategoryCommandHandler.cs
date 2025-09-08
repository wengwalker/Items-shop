using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(CatalogDbContext context, IValidator<UpdateCategoryCommand> validator) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<UpdateCategoryCommand> _validator = validator;

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CategoryEntity));

        category.Name = request.Name;
        category.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
}
