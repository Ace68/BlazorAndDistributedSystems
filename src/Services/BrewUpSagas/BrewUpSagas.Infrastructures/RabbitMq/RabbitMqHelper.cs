using BrewUpSagas.Infrastructures.RabbitMq.Commands;
using BrewUpSagas.Infrastructures.RabbitMq.Events;
using BrewUpSagas.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUpSagas.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
    public static IServiceCollection AddRabbitMqForSagasModule(this IServiceCollection services,
        RabbitMqSettings rabbitMqSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
            rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
        var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

        services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

        serviceProvider = services.BuildServiceProvider();
        var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
        var consumerConfiguration = new ConsumerConfiguration
        {
            QueueName = "StartBrewOrderSaga",
            ResourceKey = "StartBrewOrderSaga"
        };
        consumers = consumers.Concat(new List<IConsumer>
        {
            new StartBrewOrderSagaConsumer(serviceProvider, consumerConfiguration, mufloneConnectionFactory, loggerFactory),
            new ReceiveBrewOrderConsumer(mufloneConnectionFactory, loggerFactory),
            new BrewOrderApprovedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory),
            new PrepareBrewOrderConsumer(mufloneConnectionFactory , loggerFactory),
            new BrewOrderReadyToSendConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory),
            new SendBrewOrderConsumer(mufloneConnectionFactory, loggerFactory),
            new BrewOrderApprovedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory),
            new CloseBrewOrderConsumer(mufloneConnectionFactory, loggerFactory),
            new BrewOrderProcessedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory),
            new BrewOrderSagaCompletedConsumer(serviceProvider, mufloneConnectionFactory, loggerFactory),
        });
        services.AddMufloneRabbitMQConsumers(consumers);

        return services;
    }
}