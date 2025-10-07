using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductCategory;

public class UpdateProductCategory : AbstractValidator<UpdateProductCategoryRequest>
{
    public UpdateProductCategory()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");
    }
}
