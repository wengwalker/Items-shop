using Mediator.Lite.Interfaces;
using Order.Domain.Enums;

namespace Order.Application.UseCases.Orders.UpdateOrderStatus;

public record UpdateOrderStatusCommand(
    Guid OrderId,
    OrderStatus NewStatus) : IRequest<UpdateOrderStatusCommandResponse>;
