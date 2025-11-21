using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Catalogs.Features.Shared.Responses;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

public sealed record UpdateCategoryNameRequest(
    [FromRoute] Guid categoryId,
    [FromBody] string Name);

public class UpdateCategoryNameEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(CategoriesRouteConsts.UpdateCategoryName, Handle)
            .WithName("UpdateCategoryNameById")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Updates an category name")
            .WithDescription("Updates an category name by providing category id in route and name in body")
            .Produces<CategoryResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [AsParameters] UpdateCategoryNameRequest request,
        [FromServices] IValidator<UpdateCategoryNameRequest> validator,
        [FromServices] IUpdateCategoryNameHandler handler,
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
