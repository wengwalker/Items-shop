using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.CreateCartItem;

internal interface ICreateCartItemHandler : IHandler
{
    Task<Result<CartItemResponse>> HandleAsync(CreateCartItemRequest request, CancellationToken cancellationToken);
}

internal sealed class CreateCartItemHandler(
    CatalogDbContext context,
    ILogger<CreateCartItemHandler> logger)
    : ICreateCartItemHandler
{
    public async Task<Result<CartItemResponse>> HandleAsync(CreateCartItemRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating cart item for cart ({CartId}) with product {ProductId} and it's quantity {Quantity}",
            request.cartId, request.ProductId, request.Quantity);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.cartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with Id {CartId} does not exists", request.cartId);

            return Result<CartItemResponse>.Failure($"Cart with Id {request.cartId} does not exists", ErrorType.NotFound);
        }

        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<CartItemResponse>.Failure($"Product with Id {request.ProductId} does not exists", ErrorType.NotFound);
        }

        if (product.Quantity < request.Quantity)
        {
            logger.LogInformation("Required quantity ({Quantity}) exceeds the quantity of the product with Id {ProductId} of {Quantity} items",
                request.Quantity, product.Id, product.Quantity);

            return Result<CartItemResponse>
                .Failure($"Required quantity ({request.Quantity}) exceeds the quantity of the product with Id {product.Id} of {product.Quantity} items",
                    ErrorType.BadRequest);
        }

        var cartItem = request.MapToCartItem();

        context.CartItems.Add(cartItem);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created cart item for cart ({CartId}) with product {ProductId} and it's quantity {Quantity}",
            request.cartId, request.ProductId, request.Quantity);

        return Result<CartItemResponse>.Success(cartItem.MapToResponse());
    }
}
