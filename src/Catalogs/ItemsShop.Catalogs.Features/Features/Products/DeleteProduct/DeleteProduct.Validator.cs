using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.DeleteProduct;

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
