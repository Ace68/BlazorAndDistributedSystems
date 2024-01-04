using BrewUpLogistics.Messages.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUpLogistics.ReadModel.EventHandlers;

public sealed class BrewOrderSentForIntegrationEventHandler
	(ILoggerFactory loggerFactory, IEventBus eventBus) : DomainEventHandlerAsync<BrewOrderSent>(loggerFactory)
{
	public override async Task HandleAsync(BrewOrderSent @event, CancellationToken cancellationToken = new())
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await eventBus.PublishAsync(new BrewOrderProcessed(@event.BrewOrderId, correlationId), cancellationToken);
	}
}