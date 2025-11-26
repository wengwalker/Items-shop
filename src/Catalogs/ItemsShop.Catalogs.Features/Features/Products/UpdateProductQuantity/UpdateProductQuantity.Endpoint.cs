using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductQuantity;

internal sealed record UpdateProductQuantityBody(
    long Quantity);

public sealed record UpdateProductQuantityRequest(
    Guid ProductId,
    long Quantity);

public class UpdateProductQuantityEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(ProductsRouteConsts.UpdateProductQuantity, Handle)
            .WithName("UpdateProductQuantityById")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Updates an quantity in product")
            .WithDescription("Updates an quantity in product by providing product id in route and quantity in body")
            .Produces<ProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductQuantityBody body,
        [FromServices] IValidator<UpdateProductQuantityRequest> validator,
        [FromServices] IUpdateProductQuantityHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductQuantityRequest(productId, body.Quantity);

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
