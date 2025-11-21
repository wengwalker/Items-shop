using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

public class UpdateCategoryDescriptionRequestValidator : AbstractValidator<UpdateCategoryDescriptionRequest>
{
    public UpdateCategoryDescriptionRequestValidator()
    {
        RuleFor(x => x.categoryId)
            .NotEmpty()
                .WithMessage("CategoryId must be set");

        RuleFor(x => x.Description)
            .NotEmpty()
                .When(x => x.Description != null)
                .WithMessage("Description must not be empty if specified")
            .MaximumLength(150)
                .When(x => x.Description != null)
                .WithMessage("Description length exceeds 150 characters limit if specified");
    }
}
