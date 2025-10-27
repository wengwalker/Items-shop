using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Carts.GetCart;

public class GetCartRequestValidator : AbstractValidator<GetCartRequest>
{
    public GetCartRequestValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("CartId must be set");
    }
}
