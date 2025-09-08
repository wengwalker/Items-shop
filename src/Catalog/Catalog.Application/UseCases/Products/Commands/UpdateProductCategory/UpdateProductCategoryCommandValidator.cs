using FluentValidation;

namespace Catalog.Application.UseCases.Products.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId must be set");

        RuleFor(x => x.NewCategoryId)
            .NotEmpty()
            .WithMessage("NewCategoryId must be set");
    }
}
