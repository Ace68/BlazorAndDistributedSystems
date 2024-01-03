using BrewUpSagas.Messages.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Saga.Consumers;

namespace BrewUpSagas.Infrastructures.RabbitMq.Events;

public sealed class BrewOrderProcessedConsumer(IServiceProvider serviceProvider, IMufloneConnectionFactory mufloneConnectionFactory,
    ILoggerFactory loggerFactory) : SagaEventConsumerBase<BrewOrderProcessed>(mufloneConnectionFactory, loggerFactory)
{
    protected override ISagaEventHandlerAsync<BrewOrderProcessed> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaEventHandlerAsync<BrewOrderProcessed>>();
}