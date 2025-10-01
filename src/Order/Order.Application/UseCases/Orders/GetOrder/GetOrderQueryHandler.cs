using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.Orders.ListOrders;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderQueryResponse>
{
    private readonly OrderDbContext _context;

    private readonly IValidator<GetOrderQuery> _validator;

    public GetOrderQueryHandler(OrderDbContext context, IValidator<GetOrderQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<GetOrderQueryResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderEntity));

        return new GetOrderQueryResponse(
            order.Id,
            order.Status,
            order.Price,
            order.UpdatedAt,
            order.CreatedAt);
    }
}
