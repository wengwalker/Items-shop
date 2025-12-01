using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using ItemsShop.Common.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.DeleteCategory;

public sealed record DeleteCategoryRequest(Guid CategoryId);

public class DeleteCategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapDelete(CategoriesRouteConsts.DeleteCategory, Handle)
            .WithName("DeleteCategoryById")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Deletes a category")
            .WithDescription("Deletes a category by providing category id in route")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid categoryId,
        [FromServices] IValidator<DeleteCategoryRequest> validator,
        [FromServices] IDeleteCategoryHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryRequest(categoryId);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? Results.NoContent()
            : Results.Problem(
                detail: response.Description,
                statusCode: response.Error?.ToStatusCode());
    }
}
