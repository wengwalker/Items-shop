using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

internal interface IUpdateCartItemQuantityHandler : IHandler
{
    Task<Result<CartItemResponse>> HandleAsync(UpdateCartItemQuantityRequest request, CancellationToken cancellationToken);
}

internal sealed class UpdateCartItemQuantityHandler(
    CatalogDbContext context,
    ILogger<UpdateCartItemQuantityHandler> logger)
    : IUpdateCartItemQuantityHandler
{
    public async Task<Result<CartItemResponse>> HandleAsync(UpdateCartItemQuantityRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating CartItem Quantity from Cart with ID {CartId}", request.CartId);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<CartItemResponse>.Failure($"Cart with ID {request.CartId} does not exists", ErrorType.NotFound);
        }

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {CartItemId} does not exists", request.ItemId);

            return Result<CartItemResponse>.Failure($"CartItem with ID {request.ItemId} does not exists", ErrorType.NotFound);
        }

        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ItemId} does not exists", request.ItemId);

            return Result<CartItemResponse>.Failure($"Product with Id {request.ItemId} does not exists", ErrorType.NotFound);
        }

        if (product.Quantity < request.Quantity)
        {
            logger.LogInformation("Required quantity ({Quantity}) exceeds the quantity of the product with Id {ProductId} of {Quantity} items",
                request.Quantity, product.Id, product.Quantity);

            return Result<CartItemResponse>
                .Failure($"Required quantity ({request.Quantity}) exceeds the quantity of the product with Id {product.Id} of {product.Quantity} items",
                    ErrorType.BadRequest);
        }

        cartItem.Quantity = request.Quantity;

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Updated CartItem Quantity from Cart with ID {CartId}", request.CartId);

        return Result<CartItemResponse>.Success(cartItem.MapToResponse());
    }
}
