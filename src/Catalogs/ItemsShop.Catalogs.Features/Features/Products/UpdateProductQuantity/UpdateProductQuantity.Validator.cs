using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

public class UpdateProductQuantityRequestValidator : AbstractValidator<UpdateProductQuantityRequest>
{
    public UpdateProductQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set");
    }
}
