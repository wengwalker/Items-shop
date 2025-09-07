using Mediator.Lite.Interfaces;

namespace Catalog.Application.UseCases.Products.Commands.UpdateProductCategory;

public record UpdateProductCategoryCommand(
    Guid ProductId,
    Guid NewCategoryId)
    : IRequest<UpdateProductCategoryCommandResponse>;
