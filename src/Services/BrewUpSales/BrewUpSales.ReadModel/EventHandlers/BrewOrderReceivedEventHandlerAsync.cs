using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUpSales.ReadModel.EventHandlers;

public sealed class BrewOrderReceivedEventHandlerAsync(IBrewOrderService brewOrderService,
        ILoggerFactory loggerFactory)
    : DomainEventHandlerAsync<BrewOrderReceived>(loggerFactory)
{
    public override async Task HandleAsync(BrewOrderReceived @event, CancellationToken cancellationToken = new())
    {
        await brewOrderService.ReceiveBrewOrderAsync(@event.BrewOrderId, @event.BrewOrderNumber, @event.ReceivedOn,
            @event.BrewOrderBody, @event.BrewOrderStatus, cancellationToken);
    }
}