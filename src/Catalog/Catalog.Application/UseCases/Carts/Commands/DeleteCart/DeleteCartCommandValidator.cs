using FluentValidation;

namespace Catalog.Application.UseCases.Carts.Commands.DeleteCart;

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must be set");
    }
}
