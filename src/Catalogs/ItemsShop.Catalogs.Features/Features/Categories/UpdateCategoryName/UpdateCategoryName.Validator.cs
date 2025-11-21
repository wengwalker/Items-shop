using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

public class UpdateCategoryNameRequestValidator : AbstractValidator<UpdateCategoryNameRequest>
{
    public UpdateCategoryNameRequestValidator()
    {
        RuleFor(x => x.categoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must be set")
            .MaximumLength(70)
            .WithMessage("Name length exceeds 70 characters limit");
    }
}
