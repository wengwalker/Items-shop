using FluentValidation;
using ItemsShop.Catalogs.PublicApi.Contracts;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

public class GetProductRequestValidator : AbstractValidator<GetProductRequest>
{
    public GetProductRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
