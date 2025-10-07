using FluentValidation;
using ItemsShop.Catalog.Features.Shared.Routes;
using ItemsShop.Common.Api.Abstractions;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ItemsShop.Catalog.Features.Features.Products.UpdateProductCategory;

public sealed record UpdateProductCategoryRequest(
    Guid CategoryId);

public class UpdateProductCategoryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(ProductRotueConsts.UpdateProductCategory, Handle);
    }

    private static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] UpdateProductCategoryRequest request,
        IValidator<UpdateProductCategoryRequest> validator,
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var command = request.MapToCommand(id);

        var response = await mediator.Send(command, cancellationToken);

        return response.IsSuccess
            ? Results.NoContent()
            : Results.Conflict(response.Error);
    }
}
