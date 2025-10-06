using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.DeleteProduct;

public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
