using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Commands.AddCategory;

public sealed class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, AddCategoryCommandResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<AddCategoryCommand> _validator;

    public AddCategoryCommandHandler(CatalogDbContext context, IValidator<AddCategoryCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = new CategoryEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _context.Categories.AddAsync(category, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
}
