using Catalog.Application.UseCases.Carts.Commands.AddCart;
using Catalog.Application.UseCases.Carts.Commands.DeleteCart;
using Catalog.Application.UseCases.Carts.Queries.GetCart;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/v1/carts")]
[ApiController]
public class CartsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCart(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCartCommand(), cancellationToken);

        return CreatedAtAction(nameof(AddCart), response);
    }

    [Route("{id}")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCartQuery(id), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCart([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCartCommand(id), cancellationToken);

        return NoContent();
    }
}
