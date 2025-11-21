using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

public class UpdateProductCategoryRequestValidator : AbstractValidator<UpdateProductCategoryRequest>
{
    public UpdateProductCategoryRequestValidator()
    {
        RuleFor(x => x.productId)
            .NotEmpty()
            .WithMessage("ProductId must be set");

        RuleFor(x => x.categoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");
    }
}
