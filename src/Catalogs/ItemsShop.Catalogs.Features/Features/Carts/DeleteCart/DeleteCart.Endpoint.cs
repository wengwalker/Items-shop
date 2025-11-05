using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Carts.DeleteCart;

public sealed record DeleteCartRequest([FromRoute] Guid cartId);

public class DeleteCartEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(CartsRouteConsts.DeleteCart, Handle)
            .WithName("DeleteCartById")
            .WithTags(CartsTagConsts.CartsEndpointTags)
            .WithSummary("Deletes an cart")
            .WithDescription("Deletes an cart by providing cart id in route")
            .Produces<DeleteCartResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] DeleteCartRequest request,
        [FromServices] IValidator<DeleteCartRequest> validator,
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
            ? Results.NoContent()
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
