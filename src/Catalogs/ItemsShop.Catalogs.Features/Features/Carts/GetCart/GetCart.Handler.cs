using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

public sealed record GetCartCommand(Guid CartId) : IRequest<Result<GetCartResponse>>;

public sealed record GetCartResponse(
    Guid CartId,
    DateTime LastUpdated);

public sealed class GetCartHandler(
    CatalogDbContext context,
    ILogger<GetCartHandler> logger) : IRequestHandler<GetCartCommand, Result<GetCartResponse>>
{
    public async Task<Result<GetCartResponse>> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Fetching cart");

        var cart = await context.Carts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);

        if (cart == null)
        {
            logger.LogInformation("Cart with ID {CartId} does not exists", request.CartId);

            return Result<GetCartResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        var response = cart.MapToResponse();

        logger.LogInformation("Fetched cart with ID: {CartId}", response.CartId);

        return Result<GetCartResponse>.Success(response);
    }
}
