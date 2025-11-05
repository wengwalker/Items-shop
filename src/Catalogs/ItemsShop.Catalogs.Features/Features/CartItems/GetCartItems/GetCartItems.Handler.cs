using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.CartItems.GetCartItems;

public sealed record GetCartItemsQuery(Guid CartId) : IRequest<Result<GetCartItemsResponse>>;

public sealed record GetCartItemsResponse(ICollection<CartItemResponse> Items);

public sealed class GetCartItemsHandler(
    CatalogDbContext context,
    ILogger<GetCartItemsHandler> logger) : IRequestHandler<GetCartItemsQuery, Result<GetCartItemsResponse>>
{
    public async Task<Result<GetCartItemsResponse>> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching CartItems from Cart with ID {CartId}", request.CartId);

        bool cartExists = await context.Carts
            .AnyAsync(x => x.Id == request.CartId, cancellationToken);

        if (!cartExists)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<GetCartItemsResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        List<CartItemResponse> cartItems = await context.CartItems
            .AsNoTracking()
            .Where(x => x.CartId == request.CartId)
            .Select(x => new CartItemResponse(x.Id, x.Quantity, x.CartId, x.ProductId))
            .ToListAsync(cancellationToken);

        GetCartItemsResponse response = cartItems.MapToResponse();

        logger.LogInformation("Fetched CartItems from Cart with ID {CartId}", request.CartId);

        return Result<GetCartItemsResponse>.Success(response);
    }
}
