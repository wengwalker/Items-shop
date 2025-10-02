using FluentValidation;

namespace Order.Application.UseCases.OrderItems.AddOrderItem;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("Id must be set");

        RuleFor(x => x.ProductItemId)
            .NotEmpty()
            .WithMessage("ProductItemId must be set");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}
