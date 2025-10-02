using FluentValidation;

namespace Order.Application.UseCases.Orders.UpdateOrderPrice;

public class UpdateOrderPriceCommandValidator : AbstractValidator<UpdateOrderPriceCommand>
{
    public UpdateOrderPriceCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Id must be set");

        RuleFor(x => x.NewPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("NewPrice must be greater or equal to 0");
    }
}
