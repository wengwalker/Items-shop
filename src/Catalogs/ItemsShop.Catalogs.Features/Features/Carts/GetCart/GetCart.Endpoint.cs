using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Carts.GetCart;

public sealed record GetCartRequest([FromRoute] Guid cartId);

public class GetCartEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CartsRouteConsts.GetCart, Handle)
            .WithName("GetCartById")
            .WithTags(CartsTagConsts.CartsEndpointTags)
            .WithSummary("Returns one cart")
            .WithDescription("Returns one cart by providing cart id in route")
            .Produces<GetCartResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetCartRequest request,
        [FromServices] IValidator<GetCartRequest> validator,
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
            ? Results.Ok(response)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
