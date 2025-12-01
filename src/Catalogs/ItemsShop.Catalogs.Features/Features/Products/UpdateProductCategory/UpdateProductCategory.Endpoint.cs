using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductCategory;

internal sealed record UpdateProductCategoryBody(
    Guid CategoryId);

public sealed record UpdateProductCategoryRequest(
    Guid ProductId,
    Guid CategoryId);

public class UpdateProductCategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(ProductsRouteConsts.UpdateProductCategory, Handle)
            .WithName("UpdateProductCategoryById")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Updates an category in product")
            .WithDescription("Updates an category in product by providing product id in route and category id in body")
            .Produces<ProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductCategoryBody body,
        [FromServices] IValidator<UpdateProductCategoryRequest> validator,
        [FromServices] IUpdateProductCategoryHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new UpdateProductCategoryRequest(productId, body.CategoryId);

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
