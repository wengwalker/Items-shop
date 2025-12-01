using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.GetOrderItems;

public class GetOrderItemsRequestValidator : AbstractValidator<GetOrderItemsRequest>
{
    public GetOrderItemsRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");
    }
}
