using Catalog.Domain.Entities;
using Catalog.Infrastructure.Context;
using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategory;

public sealed class GetCategoryQueryHandler(CatalogDbContext context, IValidator<GetCategoryQuery> validator) : IRequestHandler<GetCategoryQuery, GetCategoryQueryResponse>
{
    private readonly CatalogDbContext _context = context;

    private readonly IValidator<GetCategoryQuery> _validator = validator;

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
