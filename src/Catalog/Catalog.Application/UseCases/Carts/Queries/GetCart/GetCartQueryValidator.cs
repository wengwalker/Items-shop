using FluentValidation;

namespace Catalog.Application.UseCases.Carts.Queries.GetCart;

public class GetCartQueryValidator : AbstractValidator<GetCartQuery>
{
    public GetCartQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
