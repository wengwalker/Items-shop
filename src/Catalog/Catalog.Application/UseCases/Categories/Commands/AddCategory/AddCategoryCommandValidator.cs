using FluentValidation;

namespace Catalog.Application.UseCases.Categories.Commands.AddCategory;

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must be set")
            .MaximumLength(70)
            .WithMessage("Name length exceeds 70 characters limit");

        RuleFor(x => x.Description)
            .MaximumLength(150)
            .WithMessage("Description length exceeds 150 characters limit");
    }
}
