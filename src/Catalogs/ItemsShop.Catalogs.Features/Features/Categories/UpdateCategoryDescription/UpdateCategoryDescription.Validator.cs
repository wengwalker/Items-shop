using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

public class UpdateCategoryDescriptionRequestValidator : AbstractValidator<UpdateCategoryDescriptionRequest>
{
    public UpdateCategoryDescriptionRequestValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
                .WithMessage("CategoryId must be set");

        RuleFor(x => x.Description)
            .NotEmpty()
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must not be empty if specified")
            .MaximumLength(150)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description length exceeds 150 characters limit if specified");
    }
}
