using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Carts.CreateCart;

public class CreateCartEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(CartsRouteConsts.BaseRoute, Handle)
            .WithName("CreateCart")
            .Produces<CreateCartResponse>();
    }

    private static async Task<IResult> Handle(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new CreateCartCommand(), cancellationToken);

        return response.IsSuccess
            ? Results.Created(CartsRouteConsts.BaseRoute, response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
