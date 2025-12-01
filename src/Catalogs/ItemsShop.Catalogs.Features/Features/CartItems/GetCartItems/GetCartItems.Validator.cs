using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

public class GetCartItemsRequestValidator : AbstractValidator<GetCartItemsRequest>
{
    public GetCartItemsRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");
    }
}
