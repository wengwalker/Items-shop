using FluentValidation;
using ItemsShop.Catalog.Domain.Enums;
using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Categories.GetCategories;

public sealed record GetCategoriesRequest(
    string? Name,
    OrderQueryType? OrderType);

public class GetCategoriesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CategoriesRouteConsts.BaseRoute, Handle)
            .WithName("GetCategories")
            .Produces<GetCategoriesResponse>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromBody] GetCategoriesRequest request,
        IValidator<GetCategoriesRequest> validator,
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
