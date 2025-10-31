using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

public class UpdateCartItemQuantityRequestValidator : AbstractValidator<UpdateCartItemQuantityRequest>
{
    public UpdateCartItemQuantityRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set");
    }
}
