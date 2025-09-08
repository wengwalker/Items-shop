using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Commands.AddCategory;

public sealed class AddCategoryCommandHandler(CatalogDbContext context, IValidator<AddCategoryCommand> validator) : IRequestHandler<AddCategoryCommand, AddCategoryCommandResponse>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<AddCategoryCommand> _validator = validator;

    public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = new CategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        _context.Categories.Add(category);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
}
