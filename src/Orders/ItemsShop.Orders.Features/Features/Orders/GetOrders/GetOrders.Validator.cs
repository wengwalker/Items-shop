using FluentValidation;

namespace ItemsShop.Orders.Features.Features.Orders.GetOrders;

public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
{
    public GetOrdersRequestValidator()
    {
        RuleFor(x => x.sortType)
            .IsInEnum()
                .When(x => x.sortType != null)
                .WithMessage("SortType cannot be invalid if specified");

        RuleFor(x => x.status)
            .IsInEnum()
                .When(x => x.status != null)
                .WithMessage("Status cannot be invalid if specified");

        RuleFor(x => x.biggerOrEqualPrice)
            .NotEmpty()
                .When(x => x.biggerOrEqualPrice != null)
                .WithMessage("BiggerOrEqualPrice cannot be empty if specified")
            .GreaterThanOrEqualTo(0)
                .When(x => x.biggerOrEqualPrice != null)
                .WithMessage("BiggerOrEqualPrice must be greater than or equal to 0");

        RuleFor(x => x.lessOrEqualPrice)
            .NotEmpty()
                .When(x => x.lessOrEqualPrice != null)
                .WithMessage("LessOrEqualPrice cannot be empty if specified")
            .GreaterThanOrEqualTo(0)
                .When(x => x.lessOrEqualPrice != null)
                .WithMessage("LessOrEqualPrice must be greater than or equal to 0");

        RuleFor(x => x.createdBefore)
            .NotEmpty()
                .When(x => x.createdBefore != null)
                .WithMessage("CreatedBefore cannot be empty if specified");

        RuleFor(x => x.createdAfter)
            .NotEmpty()
                .When(x => x.createdAfter != null)
                .WithMessage("CreatedAfter cannot be empty if specified");

        RuleFor(x => x.updatedBefore)
            .NotEmpty()
                .When(x => x.updatedBefore != null)
                .WithMessage("UpdatedBefore cannot be empty if specified");

        RuleFor(x => x.updatedAfter)
            .NotEmpty()
                .When(x => x.updatedAfter != null)
                .WithMessage("UpdatedAfter cannot be empty if specified");
    }
}
