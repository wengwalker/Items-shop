using FluentValidation;

namespace Order.Application.UseCases.OrderItems.DeleteOrderItem;

public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.OrderItemId)
            .NotEmpty()
            .WithMessage("OrderItemId must be set");
    }
}
