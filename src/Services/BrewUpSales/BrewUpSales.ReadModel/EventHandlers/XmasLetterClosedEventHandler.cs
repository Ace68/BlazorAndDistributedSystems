using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUpSales.ReadModel.EventHandlers;

public sealed class BrewOrderClosedEventHandler(ILoggerFactory loggerFactory, IBrewOrderService brewOrderService)
    : DomainEventHandlerAsync<BrewOrderClosed>(loggerFactory)
{
    public override async Task HandleAsync(BrewOrderClosed @event, CancellationToken cancellationToken = new())
    {
        await brewOrderService.CloseBrewOrderAsync(@event.BrewOrderId, cancellationToken);
    }
}