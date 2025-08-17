using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Commands.UpdateCategory;

public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<UpdateCategoryCommand> _validator;

    public UpdateCategoryCommandHandler(CatalogDbContext context, IValidator<UpdateCategoryCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CategoryEntity));

        category.Name = request.Name;
        category.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
}
