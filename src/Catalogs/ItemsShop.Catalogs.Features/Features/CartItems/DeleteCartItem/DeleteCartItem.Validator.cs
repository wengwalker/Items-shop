using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.CartItems.DeleteCartItem;

public class DeleteCartItemRequestValidator : AbstractValidator<DeleteCartItemRequest>
{
    public DeleteCartItemRequestValidator()
    {
        RuleFor(x => x.cartId)
            .NotEmpty()
            .WithMessage("CartId must be set");

        RuleFor(x => x.itemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
