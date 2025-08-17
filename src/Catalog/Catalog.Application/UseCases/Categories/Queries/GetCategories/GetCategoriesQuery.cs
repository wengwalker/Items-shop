using Catalog.Domain.Enums;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategories;

public record GetCategoriesQuery(
    string? Name,
    QueryOrderType? OrderType)
    : IRequest<GetCategoriesQueryResponse>;
