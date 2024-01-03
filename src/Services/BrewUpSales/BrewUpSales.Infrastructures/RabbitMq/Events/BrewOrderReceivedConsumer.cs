using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.ReadModel.EventHandlers;
using BrewUpSales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSales.Infrastructures.RabbitMq.Events;

public sealed class BrewOrderReceivedConsumer(IBrewOrderService brewOrderService, IEventBus eventBus,
        IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
    : DomainEventsConsumerBase<BrewOrderReceived>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<BrewOrderReceived>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<BrewOrderReceived>>
    {
        new BrewOrderReceivedEventHandlerAsync(brewOrderService, loggerFactory),
        new BrewOrderReceivedForIntegrationEventHandlerAsync(loggerFactory, eventBus)
    };
}