using FluentValidation;

namespace ItemsShop.Orders.Features.Features.OrderItems.CreateOrderItem;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("OrderId must be set");

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithMessage("Quantity must be set")
            .GreaterThan(0)
            .WithMessage("Quantity must be positive and greater than 0");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
