using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductDescription;

internal sealed record UpdateProductDescriptionBody(
    string Description);

public sealed record UpdateProductDescriptionRequest(
    Guid ProductId,
    string Description);

public class UpdateProductDescriptionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(ProductsRouteConsts.UpdateProductDescription, Handle)
            .WithName("UpdateProductDescriptionById")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Updates an description in product")
            .WithDescription("Updates an description in product by providing product id in route and description in body")
            .Produces<ProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductDescriptionBody body,
        [FromServices] IValidator<UpdateProductDescriptionRequest> validator,
        [FromServices] IUpdateProductDescriptionHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductDescriptionRequest(productId, body.Description);

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
