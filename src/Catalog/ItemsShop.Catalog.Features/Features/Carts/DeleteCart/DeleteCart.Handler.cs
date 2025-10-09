using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.Carts.DeleteCart;

public sealed record DeleteCartCommand(
    Guid CartId) : IRequest<Result<DeleteCartResponse>>;

public sealed record DeleteCartResponse();

public sealed class DeleteCartHandler(
    CatalogDbContext context,
    ILogger<DeleteCartHandler> logger) : IRequestHandler<DeleteCartCommand, Result<DeleteCartResponse>>
{
    public async Task<Result<DeleteCartResponse>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting cart with Id {CartId}", request.CartId);

        var cart = await context.Carts
            .FirstOrDefaultAsync(x => x.Id == request.CartId, cancellationToken);

        if (cart == null)
        {
            logger.LogInformation("Cart with ID {ProductId} does not exists", request.CartId);

            return Result<DeleteCartResponse>
                .Failure($"Cart with ID {request.CartId} does not exists", StatusCodes.Status404NotFound);
        }

        context.Carts.Remove(cart);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Deleted cart with Id {CartId}", request.CartId);

        return Result<DeleteCartResponse>.Success();
    }
}
