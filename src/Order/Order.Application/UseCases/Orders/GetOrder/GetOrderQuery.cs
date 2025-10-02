using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.GetOrder;

public record GetOrderQuery(Guid OrderId) : IRequest<GetOrderQueryResponse>;
