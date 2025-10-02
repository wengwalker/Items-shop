using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.OrderItems.AddOrderItem;

public record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductItemId,
    decimal Quantity) : IRequest<AddOrderItemCommandResponse>;
