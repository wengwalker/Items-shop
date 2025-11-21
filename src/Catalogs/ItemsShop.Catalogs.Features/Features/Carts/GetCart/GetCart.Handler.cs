using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

internal interface IGetCartHandler : IHandler
{
    Task<Result<CartResponse>> HandleAsync(GetCartRequest request, CancellationToken cancellationToken);
}

internal sealed class GetCartHandler(
    CatalogDbContext context,
    ILogger<GetCartHandler> logger)
    : IGetCartHandler
{
    public async Task<Result<CartResponse>> HandleAsync(GetCartRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching cart");

        var cart = await context.Carts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.cartId, cancellationToken);

        if (cart == null)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.cartId);

            return Result<CartResponse>.Failure($"Cart with ID {request.cartId} does not exists", ErrorType.NotFound);
        }

        logger.LogInformation("Fetched cart with ID: {CartId}", request.cartId);

        return Result<CartResponse>.Success(cart.MapToResponse());
    }
}
