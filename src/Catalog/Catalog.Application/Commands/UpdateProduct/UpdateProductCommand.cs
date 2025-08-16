using Mediator.Lite.Interfaces;

namespace Catalog.Application.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long StockQuantity)
    : IRequest<UpdateProductCommandResponse>;
