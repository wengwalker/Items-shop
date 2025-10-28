using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductDescription;

public sealed record UpdateProductDescriptionCommand(
    Guid ProductId,
    string Description) : IRequest<Result<UpdateProductDescriptionResponse>>;

public sealed record UpdateProductDescriptionResponse(
    Guid ProductId,
    string Description);

public sealed class UpdateProductDescriptionHandler(
    CatalogDbContext context,
    ILogger<UpdateProductDescriptionHandler> logger) : IRequestHandler<UpdateProductDescriptionCommand, Result<UpdateProductDescriptionResponse>>
{
    public async Task<Result<UpdateProductDescriptionResponse>> Handle(UpdateProductDescriptionCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with Id: {ProductId}, to new description", request.ProductId);

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<UpdateProductDescriptionResponse>
                .Failure($"Product with ID {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        product.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated product with Id: {ProductId}, to new description", request.ProductId);

        var response = product.MapToResponse();

        return Result<UpdateProductDescriptionResponse>.Success(response);
    }
}
