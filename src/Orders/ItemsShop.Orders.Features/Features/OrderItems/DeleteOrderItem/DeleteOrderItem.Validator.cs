using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.DeleteOrderItem;

public class DeleteOrderItemRequestValidator : AbstractValidator<DeleteOrderItemRequest>
{
    public DeleteOrderItemRequestValidator()
    {
        RuleFor(x => x.orderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.itemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
