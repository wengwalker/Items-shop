namespace Catalog.Application.UseCases.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommandResponse(
    Guid Id,
    string Name,
    string? Description);
