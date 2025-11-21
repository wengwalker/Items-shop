using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

public sealed record UpdateCategoryDescriptionRequest(
    [FromRoute] Guid categoryId,
    [FromBody] string? Description);

public class UpdateCategoryDescriptionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(CategoriesRouteConsts.UpdateCategoryDescription, Handle)
            .WithName("UpdateCategoryDescriptionById")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Updates an category description")
            .WithDescription("Updates an category description by providing category id in route and description in body")
            .Produces<CategoryResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] UpdateCategoryDescriptionRequest request,
        [FromServices] IValidator<UpdateCategoryDescriptionRequest> validator,
        [FromServices] IUpdateCategoryDescriptionHandler handler,
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
