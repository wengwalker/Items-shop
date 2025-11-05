using FluentValidation;
using ItemsShop.Catalogs.Domain.Enums;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

public sealed record GetCategoriesRequest([FromQuery] string? Name, [FromQuery] OrderQueryType? OrderType);

public class GetCategoriesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CategoriesConsts.BaseRoute, Handle)
            .WithName("GetCategories")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Returns list of cartegories")
            .WithDescription("Returns list of categories and accepts query params: name and order")
            .Produces<GetCategoriesResponse>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] GetCategoriesRequest request,
        [FromServices] IValidator<GetCategoriesRequest> validator,
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
