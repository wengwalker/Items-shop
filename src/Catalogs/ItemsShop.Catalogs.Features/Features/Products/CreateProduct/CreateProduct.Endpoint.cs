using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.CreateProduct;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    long Quantity,
    Guid CategoryId);

public class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ProductsRouteConsts.BaseRoute, Handle)
            .WithName("CreateProduct")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Creates a new product")
            .WithDescription("Creates a new product by providing name, description, price, quantity and category id in body")
            .Produces<ProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromBody] CreateProductRequest request,
        [FromServices] IValidator<CreateProductRequest> validator,
        [FromServices] ICreateProductHandler handler,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Created(ProductsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
