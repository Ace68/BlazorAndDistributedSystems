using BrewUpLogistics.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUpLogistics.ReadModel.EventHandlers;

public sealed class XmasPresentsSentForIntegrationEventHandler : DomainEventHandlerAsync<BrewOrderSent>
{
    private readonly IEventBus _eventBus;

    public XmasPresentsSentForIntegrationEventHandler(ILoggerFactory loggerFactory, IEventBus eventBus) : base(loggerFactory)
    {
        _eventBus = eventBus;
    }

    public override async Task HandleAsync(BrewOrderSent @event, CancellationToken cancellationToken = new())
    {
        var correlationId =
            new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

        await _eventBus.PublishAsync(new XmasLetterProcessed(@event.XmasLetterId, correlationId, @event.LetterBody), cancellationToken);
    }
}