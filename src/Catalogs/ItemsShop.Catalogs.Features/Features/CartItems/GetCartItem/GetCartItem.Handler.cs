using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItem;

public sealed record GetCartItemCommand(Guid CartId, Guid ItemId) : IRequest<Result<GetCartItemResponse>>;

public sealed record GetCartItemResponse(CartItemResponse Item);

public sealed class GetCartItemHandler(
    CatalogDbContext context,
    ILogger<GetCartItemHandler> logger) : IRequestHandler<GetCartItemCommand, Result<GetCartItemResponse>>
{
    public async Task<Result<GetCartItemResponse>> Handle(GetCartItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching CartItem with ID {ItemId} from Cart with ID {CartId}", request.ItemId, request.CartId);

        var cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<GetCartItemResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var cartItem = await context.CartItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CartId == request.CartId && x.Id == request.ItemId, cancellationToken);

        if (cartItem == null)
        {
            logger.LogInformation("CartItem with ID {ItemId} from Cart with ID {CartId} does not exists", request.ItemId, request.CartId);

            return Result<GetCartItemResponse>
                .Failure($"CartItem with ID {request.ItemId} from Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var response = cartItem.MapToResponse();

        logger.LogInformation("Fetched CartItem with ID {ItemId} from Cart with ID {CartId}", request.ItemId, request.CartId);

        return Result<GetCartItemResponse>.Success(response);
    }
}
