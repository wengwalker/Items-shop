using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductPrice;

public sealed record UpdateProductPriceCommand(
    Guid ProductId,
    decimal Price) : IRequest<Result<UpdateProductPriceResponse>>;

public sealed record UpdateProductPriceResponse(
    Guid ProductId,
    decimal Price);

public sealed class UpdateProductPriceHandler(
    CatalogDbContext context,
    ILogger<UpdateProductPriceHandler> logger) : IRequestHandler<UpdateProductPriceCommand, Result<UpdateProductPriceResponse>>
{
    public async Task<Result<UpdateProductPriceResponse>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new price", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<UpdateProductPriceResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        product.Price = request.Price;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new price", request.ProductId);

        var response = product.MapToResponse();

        return Result<UpdateProductPriceResponse>.Success(response);
    }
}
