using BrewUpLogistics.Messages.Events;
using BrewUpLogistics.ReadModel.EventHandlers;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpLogistics.Infrastructures.RabbitMq.Events;

public sealed class XmasPresentsSentConsumer(IEventBus eventbus, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : DomainEventsConsumerBase<BrewOrderSent>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BrewOrderSent>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BrewOrderSent>>
    {
        new XmasPresentsSentForIntegrationEventHandler(loggerFactory, eventbus)
    };
}