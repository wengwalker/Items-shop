using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : IRequest;
