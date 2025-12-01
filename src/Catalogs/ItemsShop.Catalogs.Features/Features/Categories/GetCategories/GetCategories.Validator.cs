using FluentValidation;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

public class GetCategoriesRequestValidator : AbstractValidator<GetCategoriesRequest>
{
    public GetCategoriesRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .When(x => x.Name != null)
                .WithMessage("Name cannot be empty if specified")
            .MaximumLength(100)
                .When(x => x.Name != null)
                .WithMessage("Name length exceeds 100 characters limit if specified");

        RuleFor(x => x.SortType)
            .IsInEnum()
                .When(x => x.SortType != null)
                .WithMessage("OrderType cannot be invalid if specified");
    }
}
