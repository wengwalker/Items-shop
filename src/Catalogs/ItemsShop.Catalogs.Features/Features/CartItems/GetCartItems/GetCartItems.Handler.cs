using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

internal interface IGetCartItemsHandler : IHandler
{
    Task<Result<List<CartItemResponse>>> HandleAsync(GetCartItemsRequest request, CancellationToken cancellationToken);
}

internal sealed class GetCartItemsHandler(
    CatalogDbContext context,
    ILogger<GetCartItemsHandler> logger)
    : IGetCartItemsHandler
{
    public async Task<Result<List<CartItemResponse>>> HandleAsync(GetCartItemsRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching CartItems from Cart with ID {CartId}", request.cartId);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.cartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.cartId);

            return Result<List<CartItemResponse>>.Failure($"Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        List<CartItemResponse> cartItems = await context.CartItems
            .AsNoTracking()
            .Where(x => x.CartId == request.cartId)
            .Select(x => x.MapToResponse())
            .ToListAsync(cancellationToken);

        logger.LogInformation("Fetched CartItems from Cart with ID {CartId}", request.cartId);

        return Result<List<CartItemResponse>>.Success(cartItems);
    }
}
