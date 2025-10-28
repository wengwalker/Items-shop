using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

public class UpdateProductCategory : AbstractValidator<UpdateProductCategoryRequest>
{
    public UpdateProductCategory()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");
    }
}
