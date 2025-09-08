using FluentValidation;

namespace Catalog.Application.UseCases.Products.Queries.GetProduct;

public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");
    }
}
