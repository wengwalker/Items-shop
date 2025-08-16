using Mediator.Lite.Interfaces;

namespace Catalog.Application.Commands.AddProduct;

public record AddProductCommand(
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    Guid CategoryId)
    : IRequest<AddProductCommandResponse>;
