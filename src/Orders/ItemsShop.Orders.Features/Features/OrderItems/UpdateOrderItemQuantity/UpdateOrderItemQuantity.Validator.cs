using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityRequestValidator : AbstractValidator<UpdateOrderItemQuantityRequest>
{
    public UpdateOrderItemQuantityRequestValidator()
    {
        RuleFor(x => x.orderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.itemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set");
    }
}
