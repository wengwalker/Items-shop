using Catalog.Api.DTOs.Products;
using Catalog.Application.UseCases.Products.Commands.AddProduct;
using Catalog.Application.UseCases.Products.Commands.DeleteProduct;
using Catalog.Application.UseCases.Products.Commands.UpdateProduct;
using Catalog.Application.UseCases.Products.Commands.UpdateProductCategory;
using Catalog.Application.UseCases.Products.Queries.GetProduct;
using Catalog.Application.UseCases.Products.Queries.GetProducts;
using Domain.Common.Enums;
using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new AddProductCommand(
                request.Name,
                request.Description,
                request.Price,
                request.StockQuantity,
                request.CategoryId),
            cancellationToken);

        return CreatedAtAction(nameof(AddProduct), response);
    }

    [Route("{id}")]
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new UpdateProductCommand(
                id,
                request.Name,
                request.Description,
                request.Price,
                request.StockQuantity),
            cancellationToken);

        return Ok(response);
    }

    [Route("{id}/category")]
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductCategory(
        [FromRoute] Guid id,
        [FromBody] UpdateProductCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new UpdateProductCategoryCommand(id, request.NewCategoryId),
            cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] string? name = null,
        [FromQuery] OrderQueryType? order = null,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetProductsQuery(name, order), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetProductQuery(id), cancellationToken);

        return Ok(response);
    }

    [Route("{id}")]
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteProductCommand(id), cancellationToken);

        return NoContent();
    }
}
