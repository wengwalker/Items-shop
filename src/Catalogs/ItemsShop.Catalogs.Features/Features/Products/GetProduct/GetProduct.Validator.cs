using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(x => x.productId)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
