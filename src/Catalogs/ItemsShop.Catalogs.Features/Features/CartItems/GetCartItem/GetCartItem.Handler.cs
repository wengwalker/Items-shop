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
        logger.LogInformation("Fetching CartItem with ID {ItemId} from Cart with ID {CartId}", request.ItemId, request.CartId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<CartItemResponse>.Failure($"Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        var cartItem = await context.CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CartId == request.CartId && x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.ItemId, request.CartId);

            return Result<CartItemResponse>.Failure($"CartItem with ID {request.ItemId} from Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        logger.LogInformation("Fetched CartItem with ID {ItemId} from Cart with ID {CartId}", request.ItemId, request.CartId);

        return Result<CartItemResponse>.Success(cartItem.MapToResponse());
    }
}
