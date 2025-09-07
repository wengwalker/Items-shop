using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(
    Guid Id)
    : IRequest;
