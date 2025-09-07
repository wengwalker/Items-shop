using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.CartItems.Commands.AddCartItem;

public record AddCartItemCommand(
    Guid CartId,
    int Quantity,
    Guid ProductId)
    : IRequest<AddCartItemCommandResponse>;
