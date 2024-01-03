using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUpSales.ReadModel.EventHandlers;

public sealed class BrewOrderReceivedForIntegrationEventHandlerAsync
    (ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<BrewOrderReceived>(loggerFactory)
{
    public override async Task HandleAsync(BrewOrderReceived @event, CancellationToken cancellationToken = new())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        await eventBus.PublishAsync(new BrewOrderApproved(@event.BrewOrderId, correlationId, @event.BrewOrderBody), cancellationToken);
    }
}