using Mediator.Lite.Interfaces;

namespace Catalog.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest;
