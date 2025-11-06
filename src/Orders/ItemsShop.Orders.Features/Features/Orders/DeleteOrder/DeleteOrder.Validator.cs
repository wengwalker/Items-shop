using FluentValidation;

namespace ItemsShop.Orders.Features.Features.Orders.DeleteOrder;

public class DeleteOrderRequestValidator : AbstractValidator<DeleteOrderRequest>
{
    public DeleteOrderRequestValidator()
    {
        RuleFor(x => x.orderId)
            .NotEmpty()
            .WithMessage("orderId must be set");
    }
}
