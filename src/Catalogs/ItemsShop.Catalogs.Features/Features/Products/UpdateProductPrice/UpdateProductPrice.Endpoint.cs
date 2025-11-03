using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductPrice;

public sealed record UpdateProductPriceRequest(
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
            .Produces<UpdateProductPriceResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductPriceRequest request,
        [FromServices] IValidator<UpdateProductPriceRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(productId);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
