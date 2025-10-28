using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.CreateCategory;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name must be set")
            .MaximumLength(70)
                .WithMessage("Name length exceeds 70 characters limit");

        RuleFor(x => x.Description)
            .NotEmpty()
                .When(x => x.Description != null)
                .WithMessage("Description cannot be empty if specified")
            .MaximumLength(150)
                .When(x => x.Description != null)
                .WithMessage("Description length exceeds 150 characters limit if specified");
    }
}
