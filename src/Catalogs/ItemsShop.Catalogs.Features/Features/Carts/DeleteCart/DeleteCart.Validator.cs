using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Carts.DeleteCart;

public class DeleteCartRequestValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");
    }
}
