using Mediator.Lite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers;

[Route("api/v1/orders/items")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }


}
