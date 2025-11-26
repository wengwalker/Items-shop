using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

public class CreateCartItemRequestValidator : AbstractValidator<CreateCartItemRequest>
{
    public CreateCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set")
            .GreaterThan(0)
            .WithMessage("Quantity must be positive and greater than 0");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
