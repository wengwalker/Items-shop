using FluentValidation;
using ItemsShop.Catalogs.Features.Shared.Consts;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalogs.Features.Features.Categories.UpdateCategoryName;

public sealed record UpdateCategoryNameRequest(
    string Name);

public class UpdateCategoryNameEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPatch(CategoriesConsts.UpdateCategoryName, Handle)
            .WithName("UpdateCategoryNameById")
            .WithTags(CategoriesTagConsts.CategoriesEndpointTags)
            .WithSummary("Updates an category name")
            .WithDescription("Updates an category name by providing category id in route and name in body")
            .Produces<UpdateCategoryNameResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid categoryId,
        [FromBody] UpdateCategoryNameRequest request,
        [FromServices] IValidator<UpdateCategoryNameRequest> validator,
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
