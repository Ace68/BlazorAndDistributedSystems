using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUpSales.ReadModel.EventHandlers;

public sealed class BrewOrderClosedForIntegrationEventHandler
    (ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<BrewOrderClosed>(loggerFactory)
{
    public override async Task HandleAsync(BrewOrderClosed @event, CancellationToken cancellationToken = new())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        await eventBus.PublishAsync(new BrewOrderSagaCompleted(@event.BrewOrderId, correlationId), cancellationToken);
    }
}