using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Categories.DeleteCategory;

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
