using Domain.Common.Exceptions;
using FluentValidation;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.Orders.UpdateOrderStatus;

public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, UpdateOrderStatusCommandResponse>
{
    private readonly OrderDbContext _context;

    private readonly IValidator<UpdateOrderStatusCommand> _validator;

    public UpdateOrderStatusCommandHandler(OrderDbContext context, IValidator<UpdateOrderStatusCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<UpdateOrderStatusCommandResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var order = await _context.Orders
            .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderEntity));

        order.Status = request.NewStatus;
        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateOrderStatusCommandResponse(order.Id, order.Status, order.UpdatedAt);
    }
}
