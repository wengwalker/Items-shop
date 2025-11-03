using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

public class UpdateProductQuantityRequestValidator : AbstractValidator<UpdateProductQuantityRequest>
{
    public UpdateProductQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}
