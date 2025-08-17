using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Infrastructure.Context;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategory;

public sealed class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
{
    private readonly CatalogDbContext _context;

    private readonly IValidator<GetCategoryQuery> _validator;

    public GetCategoryQueryHandler(CatalogDbContext context, IValidator<GetCategoryQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<GetCategoryQueryResponse> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(CategoryEntity));

        return new GetCategoryQueryResponse(category.Id, category.Name, category.Description);
    }
}
