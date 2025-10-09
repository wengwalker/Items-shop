using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.CartItems.CreateCartItem;

public sealed record CreateCartItemCommand(
    Guid CartId,
    int Quantity,
    Guid ProductId) : IRequest<Result<CreateCartItemResponse>>;

public sealed record CreateCartItemResponse(
    Guid CartItemId,
    int Quantity,
    Guid CartId,
    Guid ProductId);

public sealed class CreateCartItemHandler(
    CatalogDbContext context,
    ILogger<CreateCartItemHandler> logger) : IRequestHandler<CreateCartItemCommand, Result<CreateCartItemResponse>>
{
    public async Task<Result<CreateCartItemResponse>> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating cart item for cart ({CartId}) with product {ProductId} and it's quantity {Quantity}", request.CartId, request.ProductId, request.Quantity);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with Id {CartId} does not exists", request.CartId);

            return Result<CreateCartItemResponse>
                .Failure($"Cart with Id {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var product = await context.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

        if (product == null)
        {
            logger.LogInformation("Product with Id {ProductId} does not exists", request.ProductId);

            return Result<CreateCartItemResponse>
                .Failure($"Product with Id {request.ProductId} does not exists", StatusCodes.Status404NotFound);
        }

        if (product.Quantity < request.Quantity)
        {
            logger.LogInformation("Required quantity ({Quantity}) exceeds the quantity of the product with Id {ProductId} of {Quantity} items", request.Quantity, product.Id, product.Quantity);

            return Result<CreateCartItemResponse>
                .Failure($"Required quantity ({request.Quantity}) exceeds the quantity of the product with Id {product.Id} of {product.Quantity} items", StatusCodes.Status400BadRequest);
        }

        var cartItem = request.MapToCartItem();

        context.CartItems.Add(cartItem);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created cart item for cart ({CartId}) with product {ProductId} and it's quantity {Quantity}", request.CartId, request.ProductId, request.Quantity);

        var response = cartItem.MapToResponse();

        return Result<CreateCartItemResponse>.Success(response);
    }
}
