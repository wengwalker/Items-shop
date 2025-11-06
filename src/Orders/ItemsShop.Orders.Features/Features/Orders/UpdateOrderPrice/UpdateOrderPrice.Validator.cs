using FluentValidation;

namespace ItemsShop.Orders.Features.Features.Orders.UpdateOrderPrice;

public class UpdateOrderPriceRequestValidator : AbstractValidator<UpdateOrderPriceRequest>
{
    public UpdateOrderPriceRequestValidator()
    {
        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price must be set");
    }
}
