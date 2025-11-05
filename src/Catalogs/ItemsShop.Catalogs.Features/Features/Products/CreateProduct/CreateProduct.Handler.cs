using ItemsShop.Catalogs.Infrastructure.Database;
using ItemsShop.Common.Domain.Results;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ItemsShop.Catalogs.Features.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    Guid CategoryId) : IRequest<Result<CreateProductResponse>>;

public sealed record CreateProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid CategoryId);

public sealed class CreateProductHandler(
    CatalogDbContext context,
    ILogger<CreateProductHandler> logger)
    : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
{
    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating product with name: {Name}", request.Name);

        bool categoryExists = await context.Categories
            .AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!categoryExists)
        {
            logger.LogInformation("Category with Id {CategoryId} does not exists", request.CategoryId);

            return Result<CreateProductResponse>
                .Failure($"Category with ID {request.CategoryId} does not exists", StatusCodes.Status404NotFound);
        }

        var product = request.MapToProduct();

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Created product with Id {ProductId}", product.Id);

        var response = product.MapToResponse();

        return Result<CreateProductResponse>.Success(response);
    }
}
