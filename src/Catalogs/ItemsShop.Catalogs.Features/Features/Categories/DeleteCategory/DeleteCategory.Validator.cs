using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.DeleteCategory;

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.categoryId)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
