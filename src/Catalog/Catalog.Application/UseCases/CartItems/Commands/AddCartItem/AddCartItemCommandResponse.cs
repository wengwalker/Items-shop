namespace Catalog.Application.UseCases.CartItems.Commands.AddCartItem;

public record AddCartItemCommandResponse(
    Guid Id,
    int Quantity,
    Guid CartId,
    Guid ProductId);
