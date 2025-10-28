using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.GetProduct;

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
