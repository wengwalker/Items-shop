using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.UpdateCartItemQuantity;

public sealed record UpdateCartItemQuantityCommand(
    Guid CartId,
    Guid ItemId,
    int Quantity) : IRequest<Result<UpdateCartItemQuantityResponse>>;

public sealed record UpdateCartItemQuantityResponse(
    Guid ItemId,
    Guid CartId,
    int Quantity,
    Guid ProductId);

public sealed class UpdateCartItemQuantityHandler(
    CatalogDbContext context,
    ILogger<UpdateCartItemQuantityHandler> logger) : IRequestHandler<UpdateCartItemQuantityCommand, Result<UpdateCartItemQuantityResponse>>
{
    public async Task<Result<UpdateCartItemQuantityResponse>> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating CartItem Quantity from Cart with ID {CartId}", request.CartId);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<UpdateCartItemQuantityResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {CartItemId} does not exists", request.ItemId);

            return Result<UpdateCartItemQuantityResponse>
                .Failure($"CartItem with ID {request.ItemId} does not exists", StatusCodes.Status404NotFound);
        }

        cartItem.Quantity = request.Quantity;

        await context.SaveChangesAsync(cancellationToken);

        var response = cartItem.MapToResponse();

        logger.LogInformation("Updated CartItem Quantity from Cart with ID {CartId}", request.CartId);

        return Result<UpdateCartItemQuantityResponse>.Success(response);
    }
}
