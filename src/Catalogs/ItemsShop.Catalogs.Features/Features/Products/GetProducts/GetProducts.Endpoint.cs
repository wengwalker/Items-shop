using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Common.Application.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProducts;

public sealed record GetProductsRequest(
    [FromQuery] string? Name,
    [FromQuery] QuerySortType? SortType);

public class GetProductsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(ProductsRouteConsts.BaseRoute, Handle)
            .WithName("GetProducts")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Returns list of products")
            .WithDescription("Returns list of products and accepts query params: name and order")
            .Produces<List<ProductResponse>>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetProductsRequest request,
        [FromServices] IValidator<GetProductsRequest> validator,
        [FromServices] IGetProductsHandler handler,
        CancellationToken cancellationToken)
    {
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
