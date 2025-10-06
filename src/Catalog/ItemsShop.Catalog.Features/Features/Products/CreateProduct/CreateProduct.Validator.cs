using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must be set")
            .MaximumLength(100)
            .WithMessage("Name length exceeds 100 characters limit");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description must be set")
            .MaximumLength(100)
            .WithMessage("Description length exceeds 300 characters limit");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price must be set");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("StockQuantity must be set");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("CategoryId must be set");
    }
}
