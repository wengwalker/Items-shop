using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductPrice;

internal sealed record UpdateProductPriceBody(
    decimal Price);

public sealed record UpdateProductPriceRequest(
    Guid ProductId,
    decimal Price);

public class UpdateProductPriceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(ProductsRouteConsts.UpdateProductPrice, Handle)
            .WithName("UpdateProductPriceById")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Updates an price in product")
            .WithDescription("Updates an price in product by providing product id in route and price in body")
            .Produces<ProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductPriceBody body,
        [FromServices] IValidator<UpdateProductPriceRequest> validator,
        [FromServices] IUpdateProductPriceHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductPriceRequest(productId, body.Price);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
