using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.CartItems.Commands.DeleteCartItem;

public record DeleteCartItemCommand(
    Guid CartId,
    Guid ItemId)
    : IRequest;
