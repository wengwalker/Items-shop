using Mediator.Lite.Interfaces;

namespace Order.Application.UseCases.Orders.UpdateOrderPrice;

public class UpdateOrderPriceCommandHandler : IRequestHandler<UpdateOrderPriceCommand, UpdateOrderPriceCommandResponse>
{
    public Task<UpdateOrderPriceCommandResponse> Handle(UpdateOrderPriceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
