using FluentValidation;
using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductPrice;

public sealed record UpdateProductPriceRequest(
    decimal Price);

public class UpdateProductPriceEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(ProductRouteConsts.UpdateProductPrice, Handle)
            .WithName("UpdateProductPriceById")
            .Produces<UpdateProductPriceResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] UpdateProductPriceRequest request,
        IValidator<UpdateProductPriceRequest> validator,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(id);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
