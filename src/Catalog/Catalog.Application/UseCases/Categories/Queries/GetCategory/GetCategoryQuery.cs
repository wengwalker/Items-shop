using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategory;

public record GetCategoryQuery(
    Guid Id)
    : IRequest<GetCategoryQueryResponse>;
