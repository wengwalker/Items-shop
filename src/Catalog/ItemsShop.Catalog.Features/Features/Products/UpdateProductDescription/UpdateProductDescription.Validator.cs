using FluentValidation;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductDescription;

public class UpdateProductDescriptionRequestValidator : AbstractValidator<UpdateProductDescriptionRequest>
{
    public UpdateProductDescriptionRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description must be set");
    }
}
