using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductDescription;

public class UpdateProductDescriptionRequestValidator : AbstractValidator<UpdateProductDescriptionRequest>
{
    public UpdateProductDescriptionRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Description must be set")
            .MaximumLength(300)
                .WithMessage("Description length exceeds 300 characters limit");
    }
}
