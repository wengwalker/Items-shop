using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItem;

public class GetOrderItemRequestValidator : AbstractValidator<GetOrderItemRequest>
{
    public GetOrderItemRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("ItemId must be set");
    }
}
