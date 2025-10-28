using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductPrice;

public class UpdateProductPriceRequestValidator : AbstractValidator<UpdateProductPriceRequest>
{
    public UpdateProductPriceRequestValidator()
    {
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price must be set");
    }
}
