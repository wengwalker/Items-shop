using Catalog.Api.DTOs.CartItems;
using Catalog.Application.UseCases.CartItems.Commands.AddCartItem;
using Catalog.Application.UseCases.CartItems.Commands.DeleteCartItem;
using Catalog.Application.UseCases.CartItems.Queries.GetCartItems;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/v1/carts/{cartId}/items")]
[ApiController]
public class CartItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddCartItem([FromRoute] Guid cartId, [FromBody] AddCartItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new AddCartItemCommand(cartId, request.Quantity, request.ProductId), cancellationToken);

        return CreatedAtAction(nameof(AddCartItem), new { CartId = cartId }, response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCartItems([FromRoute] Guid cartId, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCartItemsQuery(cartId), cancellationToken);

        return Ok(response);
    }

    [Route("{itemId}")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCartItem([FromRoute] Guid cartId, [FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCartItemCommand(cartId, itemId), cancellationToken);

        return NoContent();
    }
}
