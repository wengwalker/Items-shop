using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.DeleteOrderItem;

public class DeleteOrderItemRequestValidator : AbstractValidator<DeleteOrderItemRequest>
{
    public DeleteOrderItemRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
