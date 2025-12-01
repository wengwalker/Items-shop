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
        logger.LogInformation("Deleting CartItem with Id: {ItemId}", request.ItemId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result.Failure($"Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.ItemId, request.CartId);

            return Result.Failure($"CartItem with ID {request.ItemId} from Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        context.CartItems.Remove(cartItem!);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted CartItem with Id: {ItemId}", request.ItemId);

        return Result.Success();
    }
}
