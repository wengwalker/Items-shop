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
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCategoryCommand(request.Name, request.Description), cancellationToken);

        return CreatedAtAction(nameof(AddCategory), response);
    }

    [Route("{id}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateCategoryCommand(id, request.Name, request.Description), cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] string? name = null, [FromQuery] QueryOrderType? order = null, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCategoriesQuery(name, order), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCategoryQuery(id), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);

        return NoContent();
    }
}
