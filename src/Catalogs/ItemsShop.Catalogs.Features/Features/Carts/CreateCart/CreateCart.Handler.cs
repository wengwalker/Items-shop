using ItemsShop.Catalogs.Domain.Entities;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Handlers;
using ItemsShop.Common.Domain.Results;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Carts.CreateCart;

internal interface ICreateCartHandler : IHandler
{
    Task<Result<CartResponse>> HandleAsync(CancellationToken cancellationToken);
}

internal sealed class CreateCartHandler(
    CatalogDbContext context,
    ILogger<CreateCartHandler> logger)
    : ICreateCartHandler
{
    public async Task<Result<CartResponse>> HandleAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating cart entity");

        var cart = new CartEntity
        {
            Id = Guid.NewGuid(),
            LastUpdated = DateTime.UtcNow
        };

        context.Carts.Add(cart);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created cart entity with Id {CartId}", cart.Id);

        return Result<CartResponse>.Success(cart.MapToResponse());
    }
}
