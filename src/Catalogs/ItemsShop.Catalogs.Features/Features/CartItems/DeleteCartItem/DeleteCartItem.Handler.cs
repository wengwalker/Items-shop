using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.DeleteCartItem;

internal interface IDeleteCartItemHandler : IHandler
{
    Task<Result> HandleAsync(DeleteCartItemRequest request, CancellationToken cancellationToken);
}

internal sealed class DeleteCartItemHandler(
    CatalogDbContext context,
    ILogger<DeleteCartItemHandler> logger)
    : IDeleteCartItemHandler
{
    public async Task<Result> HandleAsync(DeleteCartItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting CartItem with Id: {ItemId}", request.itemId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.cartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.cartId);

            return Result.Failure($"Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.itemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.itemId, request.cartId);

            return Result.Failure($"CartItem with ID {request.itemId} from Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        context.CartItems.Remove(cartItem!);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted CartItem with Id: {ItemId}", request.itemId);

        return Result.Success();
    }
}
