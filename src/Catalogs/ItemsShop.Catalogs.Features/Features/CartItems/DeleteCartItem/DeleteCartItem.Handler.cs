using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.DeleteCartItem;

public sealed record DeleteCartItemCommand(
    Guid CartId,
    Guid ItemId) : IRequest<Result<DeleteCartItemResponse>>;

public sealed record DeleteCartItemResponse();

public sealed class DeleteCartItemHandler(
    CatalogDbContext context,
    ILogger<DeleteCartItemHandler> logger) : IRequestHandler<DeleteCartItemCommand, Result<DeleteCartItemResponse>>
{
    public async Task<Result<DeleteCartItemResponse>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting CartItem with Id: {ItemId}", request.ItemId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<DeleteCartItemResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var cartItem = await context.CartItems
            .FirstOrDefaultAsync(x => x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.ItemId, request.CartId);

            return Result<DeleteCartItemResponse>
                .Failure($"CartItem with ID {request.ItemId} from Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        context.CartItems.Remove(cartItem!);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted CartItem with Id: {ItemId}", request.ItemId);

        return Result<DeleteCartItemResponse>.Success();
    }
}
