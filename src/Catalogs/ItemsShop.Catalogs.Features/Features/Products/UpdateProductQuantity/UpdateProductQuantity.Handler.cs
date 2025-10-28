using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductQuantity;

public sealed record UpdateProductQuantityCommand(
    Guid ProductId,
    long Quantity) : IRequest<Result<UpdateProductQuantityResponse>>;

public sealed record UpdateProductQuantityResponse(
    Guid ProductId,
    long Quantity);

public sealed class UpdateProductQuantityHandler(
    CatalogDbContext context,
    ILogger<UpdateProductQuantityHandler> logger) : IRequestHandler<UpdateProductQuantityCommand, Result<UpdateProductQuantityResponse>>
{
    public async Task<Result<UpdateProductQuantityResponse>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new stock quantity", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<UpdateProductQuantityResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        product.Quantity = request.Quantity;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new stock quantity", request.ProductId);

        var response = product.MapToResponse();

        return Result<UpdateProductQuantityResponse>.Success(response);
    }
}
