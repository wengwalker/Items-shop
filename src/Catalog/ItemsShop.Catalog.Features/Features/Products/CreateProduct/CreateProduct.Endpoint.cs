using FluentValidation;
using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Products.CreateProduct;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    long StockQuantity,
    Guid CategoryId);

public class CreateProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ProductRouteConsts.BaseRoute, Handle)
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromBody] CreateProductRequest request,
        IValidator<CreateProductRequest> validator,
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
            ? Results.Created(ProductRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
