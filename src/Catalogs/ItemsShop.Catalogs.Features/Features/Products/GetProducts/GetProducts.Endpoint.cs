using FluentValidation;
using ItemsShop.Catalogs.Domain.Enums;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProducts;

public sealed record GetProductsRequest([FromQuery] string? Name, [FromQuery] OrderQueryType? OrderType);

public class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(ProductsRouteConsts.BaseRoute, Handle)
            .WithName("GetProducts")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Returns list of products")
            .WithDescription("Returns list of products and accepts query params: name and order")
            .Produces<GetProductsResponse>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetProductsRequest request,
        [FromServices] IValidator<GetProductsRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand();

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
