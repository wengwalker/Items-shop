using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Carts.DeleteCart;

internal interface IDeleteCartHandler : IHandler
{
    Task<Result> HandleAsync(DeleteCartRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteCartHandler(
    CatalogDbContext context,
    ILogger<DeleteCartHandler> logger)
    : IDeleteCartHandler
{
    public async Task<Result> HandleAsync(DeleteCartRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cart with Id {CartId}", request.CartId);

        var cart = await context.Carts
            .FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);

        if (cart == null)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result.Failure($"Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        context.Carts.Remove(cart);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted cart with Id {CartId}", request.CartId);

        return Result.Success();
    }
}
