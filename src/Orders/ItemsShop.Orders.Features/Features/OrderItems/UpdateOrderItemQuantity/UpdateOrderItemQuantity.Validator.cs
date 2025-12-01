using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.UpdateOrderItemQuantity;

public class UpdateOrderItemQuantityRequestValidator : AbstractValidator<UpdateOrderItemQuantityRequest>
{
    public UpdateOrderItemQuantityRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set");
    }
}
