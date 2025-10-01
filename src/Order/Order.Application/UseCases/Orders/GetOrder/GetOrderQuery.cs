using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.ListOrders;

public record GetOrderQuery(Guid OrderId) : IRequest<GetOrderQueryResponse>;
