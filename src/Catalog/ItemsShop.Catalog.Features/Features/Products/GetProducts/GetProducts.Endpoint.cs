using FluentValidation;
using ItemsShop.Catalog.Domain.Enums;
using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Products.GetProducts;

public sealed record GetProductsRequest(
    string? Name,
    OrderQueryType? OrderType);

public class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(ProductRouteConsts.BaseRoute, Handle)
            .WithName("GetProducts")
            .Produces<GetProductsResponse>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromBody] GetProductsRequest request,
        IValidator<GetProductsRequest> validator,
        IMediator mediator,
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
