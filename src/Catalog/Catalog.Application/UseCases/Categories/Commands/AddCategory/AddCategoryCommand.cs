using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Categories.Commands.AddCategory;

public record AddCategoryCommand(
    string Name,
    string? Description)
    : IRequest<AddCategoryCommandResponse>;
