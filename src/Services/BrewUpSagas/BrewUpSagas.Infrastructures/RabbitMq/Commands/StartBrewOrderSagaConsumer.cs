using BrewUpSagas.Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Saga;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Models;
using Muflone.Transport.RabbitMQ.Saga.Consumers;

namespace BrewUpSagas.Infrastructures.RabbitMq.Commands;

public class StartBrewOrderSagaConsumer(IServiceProvider serviceProvider, ConsumerConfiguration configuration, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : SagaStartedByConsumerBase<StartBrewOrderSaga>(configuration, connectionFactory, loggerFactory)
{
    protected override ISagaStartedByAsync<StartBrewOrderSaga> HandlerAsync { get; } = serviceProvider.GetRequiredService<ISagaStartedByAsync<StartBrewOrderSaga>>();
}