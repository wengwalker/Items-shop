using FluentValidation;

namespace Catalog.Application.UseCases.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must be set");

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
