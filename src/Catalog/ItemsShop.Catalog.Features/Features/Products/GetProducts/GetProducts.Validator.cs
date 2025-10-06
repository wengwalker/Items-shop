using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.GetProducts;

public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    public GetProductsRequestValidator()
    {
        RuleFor(x => x.Name)
    }
}
