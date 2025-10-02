using Domain.Common.Exceptions;
using FluentValidation;
using Infrastructure.Common.ExternalApi.Interfaces;
using Mediator.Lite.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using Order.Infrastructure.Context;

namespace Order.Application.UseCases.OrderItems.AddOrderItem;

public sealed class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, AddOrderItemCommandResponse>
{
    private readonly OrderDbContext _context;

    private readonly IValidator<AddOrderItemCommand> _validator;

    private readonly ICatalogApi _catalogApi;

    public AddOrderItemCommandHandler(OrderDbContext context, IValidator<AddOrderItemCommand> validator, ICatalogApi catalogApi)
    {
        _context = context;
        _validator = validator;
        _catalogApi = catalogApi;
    }

    public async Task<AddOrderItemCommandResponse> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var productItemPriceResponse = await _catalogApi.GetItemPrice(request.ProductItemId);

        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(OrderEntity));

        var orderItem = new OrderItemEntity
        {
            Id = Guid.NewGuid(),
            ProductItemId = productItemPriceResponse.ProductItemId,
            ProductItemPrice = productItemPriceResponse.Price,
            ItemsQuantity = request.Quantity,
            ItemsPrice = request.Quantity * productItemPriceResponse.Price,
            Order = order
        };

        _context.OrderItems.Add(orderItem);

        order.OrderItems.Add(orderItem);

        order.Price += orderItem.ItemsPrice;

        await _context.SaveChangesAsync(cancellationToken);

        return new AddOrderItemCommandResponse(
            orderItem.Id,
            orderItem.ProductItemId,
            orderItem.ProductItemPrice,
            orderItem.ItemsQuantity,
            orderItem.ItemsPrice,
            order.Id);
    }
}
