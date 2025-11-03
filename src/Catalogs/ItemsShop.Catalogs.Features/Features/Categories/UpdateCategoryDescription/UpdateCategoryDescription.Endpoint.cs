using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryDescription;

public sealed record UpdateCategoryDescriptionRequest(
    string? Description);

public class UpdateCategoryDescriptionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(CategoriesConsts.UpdateCategoryDescription, Handle)
            .WithName("UpdateCategoryDescriptionById")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Updates an category description")
            .WithDescription("Updates an category description by providing category id in route and description in body")
            .Produces<UpdateCategoryDescriptionResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid categoryId,
        [FromBody] UpdateCategoryDescriptionRequest request,
        [FromServices] IValidator<UpdateCategoryDescriptionRequest> validator,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(categoryId);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.Ok(response.Value)
            : Results.Problem(
                detail: response.Error,
                statusCode: response.StatusCode);
    }
}
