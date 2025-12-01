using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

public class GetCartItemRequestValidator : AbstractValidator<GetCartItemRequest>
{
    public GetCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
