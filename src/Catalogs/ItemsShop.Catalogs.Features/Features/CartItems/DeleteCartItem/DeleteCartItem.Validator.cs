using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.CartItems.DeleteCartItem;

public class DeleteCartItemRequestValidator : AbstractValidator<DeleteCartItemRequest>
{
    public DeleteCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
