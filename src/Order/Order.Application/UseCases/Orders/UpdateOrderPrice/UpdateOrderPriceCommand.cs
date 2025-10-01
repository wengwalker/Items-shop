using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.UpdateOrderPrice;

public record UpdateOrderPriceCommand(
    Guid OrderId,
    decimal NewPrice) : IRequest<UpdateOrderPriceCommandResponse>;
