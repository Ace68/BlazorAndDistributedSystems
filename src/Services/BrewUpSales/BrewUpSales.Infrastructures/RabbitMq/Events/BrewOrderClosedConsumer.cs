using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.ReadModel.EventHandlers;
using BrewUpSales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSales.Infrastructures.RabbitMq.Events;

public sealed class BrewOrderClosedConsumer(IBrewOrderService brewOrderService, IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<BrewOrderClosed>(connectionFactory,
	loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<BrewOrderClosed>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BrewOrderClosed>>
	{
		new BrewOrderClosedEventHandler(loggerFactory, brewOrderService),
		new BrewOrderClosedForIntegrationEventHandler(loggerFactory, eventBus)
	};
}