using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.PublicApi.Contracts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Products.GetProduct;

public class GetProductEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(ProductsRouteConsts.GetProduct, Handle)
            .WithName("GetProductById")
            .WithTags(ProductsTagConsts.ProductsEndpointTags)
            .WithSummary("Returns one product")
            .WithDescription("Returns one product by providing product id in route")
            .Produces<GetProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid productId,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetProductQuery(productId), cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
