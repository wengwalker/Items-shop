using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

internal interface IGetCartItemHandler : IHandler
{
    Task<Result<CartItemResponse>> HandleAsync(GetCartItemRequest request, CancellationToken cancellationToken);
}

internal sealed class GetCartItemHandler(
    CatalogDbContext context,
    ILogger<GetCartItemHandler> logger)
    : IGetCartItemHandler
{
    public async Task<Result<CartItemResponse>> HandleAsync(GetCartItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching CartItem with ID {ItemId} from Cart with ID {CartId}", request.itemId, request.cartId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.cartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.cartId);

            return Result<CartItemResponse>.Failure($"Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        var cartItem = await context.CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CartId == request.cartId && x.Id == request.itemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.itemId, request.cartId);

            return Result<CartItemResponse>.Failure($"CartItem with ID {request.itemId} from Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        logger.LogInformation("Fetched CartItem with ID {ItemId} from Cart with ID {CartId}", request.itemId, request.cartId);

        return Result<CartItemResponse>.Success(cartItem.MapToResponse());
    }
}
