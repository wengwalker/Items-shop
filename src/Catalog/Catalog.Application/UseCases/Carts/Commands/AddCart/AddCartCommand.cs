using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Carts.Commands.AddCart;

public record AddCartCommand()
    : IRequest<AddCartCommandResponse>;
