using FluentValidation;

namespace Catalog.Application.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must be set");

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
    }
}
