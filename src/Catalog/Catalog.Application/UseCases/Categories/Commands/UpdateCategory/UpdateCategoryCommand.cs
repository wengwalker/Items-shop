using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description)
    : IRequest<UpdateCategoryCommandResponse>;
