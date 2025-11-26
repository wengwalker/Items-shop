using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using ItemsShop.Common.Application.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.GetCategories;

public sealed record GetCategoriesRequest(
    string? Name,
    QuerySortType? SortType);

public class GetCategoriesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(CategoriesRouteConsts.BaseRoute, Handle)
            .WithName("GetCategories")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Returns list of cartegories")
            .WithDescription("Returns list of categories and accepts query params: name and order")
            .Produces<List<CategoryResponse>>()
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromQuery] string? name,
        [FromQuery] QuerySortType? sortType,
        [FromServices] IValidator<GetCategoriesRequest> validator,
        [FromServices] IGetCategoriesHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new GetCategoriesRequest(name, sortType);

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
