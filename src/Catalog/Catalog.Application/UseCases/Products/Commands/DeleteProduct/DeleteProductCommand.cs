using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : IRequest;
