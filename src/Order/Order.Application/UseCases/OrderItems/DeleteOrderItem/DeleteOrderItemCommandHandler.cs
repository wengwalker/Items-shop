using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.OrderItems.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
{
    private readonly OrderDbContext _context;

    private readonly IValidator<DeleteOrderItemCommand> _validator;

    public DeleteOrderItemCommandHandler(OrderDbContext context, IValidator<DeleteOrderItemCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderEntity));

        var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == request.OrderItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderItemEntity));

        order.OrderItems.Remove(orderItem);
        order.Price -= orderItem.ItemsPrice;

        _context.OrderItems.Remove(orderItem);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
