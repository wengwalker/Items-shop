using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.GetProducts;

public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    public GetProductsRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .When(x => x.Name != null)
                .WithMessage("Name cannot be empty if specified")
            .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Name length exceeds 100 characters limit");

        RuleFor(x => x.OrderType)
            .IsInEnum()
                .When(x => x.OrderType != null)
                .WithMessage("OrderType cannot be invalid if specified");
    }
}
