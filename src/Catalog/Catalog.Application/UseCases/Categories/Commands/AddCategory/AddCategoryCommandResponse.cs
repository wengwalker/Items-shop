namespace Catalog.Application.UseCases.Categories.Commands.AddCategory;

public record AddCategoryCommandResponse(
    Guid Id,
    string Name,
    string? Description);
