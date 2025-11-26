using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

public class UpdateProductCategoryRequestValidator : AbstractValidator<UpdateProductCategoryRequest>
{
    public UpdateProductCategoryRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");
    }
}
