using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.UpdateProductDescription;

public sealed record UpdateProductDescriptionRequest(
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
            .Produces<UpdateProductDescriptionResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductDescriptionRequest request,
        [FromServices] IValidator<UpdateProductDescriptionRequest> validator,
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
