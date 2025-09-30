using Mediator.Lite.Interfaces;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.Orders.AddOrder;

public sealed class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, AddOrderCommandResponse>
{
    private readonly OrderDbContext _context;

    public AddOrderCommandHandler(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<AddOrderCommandResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new OrderEntity
        {
            Id = Guid.NewGuid(),
            Price = 0
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return new AddOrderCommandResponse(order.Id);
    }
}
