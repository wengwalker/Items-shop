using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

public sealed record GetCartRequest(Guid CartId);

public class GetCartEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CartsRouteConsts.GetCart, Handle)
            .WithName("GetCartById")
            .WithTags(CartsTagConsts.CartsEndpointTags)
            .WithSummary("Returns one cart")
            .WithDescription("Returns one cart by providing cart id in route")
            .Produces<CartResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid cartId,
        [FromServices] IValidator<GetCartRequest> validator,
        [FromServices] IGetCartHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new GetCartRequest(cartId);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response)
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
