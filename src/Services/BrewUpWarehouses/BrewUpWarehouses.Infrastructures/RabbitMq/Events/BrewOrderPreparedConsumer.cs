using BrewUpWarehouses.Messages.Events;
using BrewUpWarehouses.ReadModel.EventHandlers;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpWarehouses.Infrastructures.RabbitMq.Events;

public sealed class BrewOrderPreparedConsumer(IEventBus eventbus, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : DomainEventsConsumerBase<BrewOrderPrepared>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BrewOrderPrepared>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BrewOrderPrepared>>
    {
        new BrewOrderPreparedForIntegrationEventHandler(loggerFactory, eventbus)
    };
}