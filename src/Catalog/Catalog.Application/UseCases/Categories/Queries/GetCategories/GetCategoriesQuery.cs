using Domain.Common.Enums;
using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Queries.GetCategories;

public record GetCategoriesQuery(
    string? Name,
    OrderQueryType? OrderType)
    : IRequest<GetCategoriesQueryResponse>;
