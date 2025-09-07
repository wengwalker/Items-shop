using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Carts.Commands.DeleteCart;

public record DeleteCartCommand(
    Guid Id)
    : IRequest;
