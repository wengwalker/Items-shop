using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductPrice;

public class UpdateProductPriceRequestValidator : AbstractValidator<UpdateProductPriceRequest>
{
    public UpdateProductPriceRequestValidator()
    {
        RuleFor(x => x.productId)
            .NotEmpty()
            .WithMessage("ProductId must be set");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must greater than 0");
    }
}
