using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.AddOrder;

public record AddOrderCommand() : IRequest<AddOrderCommandResponse>;
