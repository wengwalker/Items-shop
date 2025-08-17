using Catalog.Api.DTOs.Categories;
using Catalog.Application.UseCases.Categories.Commands.AddCategory;
using Catalog.Application.UseCases.Categories.Commands.DeleteCategory;
using Catalog.Application.UseCases.Categories.Commands.UpdateCategory;
using Catalog.Application.UseCases.Categories.Queries.GetCategories;
using Catalog.Application.UseCases.Categories.Queries.GetCategory;
using Catalog.Domain.Enums;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCategoryCommand(request.Name, request.Description), cancellationToken);

        return CreatedAtAction(nameof(AddCategory), response);
    }

    [Route("{id}")]
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCategoryCommand(id, request.Name, request.Description), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategories([FromQuery] string? name = null, [FromQuery] OrderQueryType? order = null, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCategoriesQuery(name, order), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoryQuery(id), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        return NoContent();
    }
}
