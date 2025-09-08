using FluentValidation;

namespace Catalog.Application.UseCases.CartItems.Queries.GetCartItems;

public class GetCartItemsQueryValidator : AbstractValidator<GetCartItemsQuery>
{
    public GetCartItemsQueryValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("CartId must be set");
    }
}
