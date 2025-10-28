using ItemsShop.Catalog.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalog.Features.Features.Carts.CreateCart;

public sealed record CreateCartCommand() : IRequest<Result<CreateCartResponse>>;

public sealed record CreateCartResponse(
    Guid CartId,
    DateTime LastUpdated);

public sealed class CreateCartHandler(
    CatalogDbContext context,
    ILogger<CreateCartHandler> logger) : IRequestHandler<CreateCartCommand, Result<CreateCartResponse>>
{
    public async Task<Result<CreateCartResponse>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating cart entity");

        var cart = request.MapToCart();

        context.Carts.Add(cart);

        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created cart entity with Id {CartId}", cart.Id);

        var response = cart.MapToResponse();

        return Result<CreateCartResponse>.Success(response);
    }
}
