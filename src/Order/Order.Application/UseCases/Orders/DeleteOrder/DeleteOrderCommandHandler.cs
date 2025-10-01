using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.Orders.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly OrderDbContext _context;

    private readonly IValidator<DeleteOrderCommand> _validator;

    public DeleteOrderCommandHandler(OrderDbContext context, IValidator<DeleteOrderCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderEntity));

        _context.Orders.Remove(order);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
