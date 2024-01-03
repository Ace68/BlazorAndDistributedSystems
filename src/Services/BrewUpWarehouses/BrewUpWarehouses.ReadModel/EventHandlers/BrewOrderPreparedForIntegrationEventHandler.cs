using BrewUpWarehouses.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUpWarehouses.ReadModel.EventHandlers;

public sealed class BrewOrderPreparedForIntegrationEventHandler : DomainEventHandlerAsync<BrewOrderPrepared>
{
    private readonly IEventBus _eventBus;

    public BrewOrderPreparedForIntegrationEventHandler(ILoggerFactory loggerFactory, IEventBus eventBus) : base(loggerFactory)
    {
        _eventBus = eventBus;
    }

    public override async Task HandleAsync(BrewOrderPrepared @event, CancellationToken cancellationToken = new())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        await _eventBus.PublishAsync(new BrewOrderReadyToSend(@event.BrewOrderId, correlationId, @event.BrewOrderBody), cancellationToken);
    }
}