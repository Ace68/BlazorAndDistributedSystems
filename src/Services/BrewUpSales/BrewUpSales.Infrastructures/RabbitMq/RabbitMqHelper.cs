using BrewUpSales.Infrastructures.RabbitMq.Commands;
using BrewUpSales.Infrastructures.RabbitMq.Events;
using BrewUpSales.ReadModel.Services;
using BrewUpSales.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUpSales.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
    public static IServiceCollection AddRabbitMqForSagasModule(this IServiceCollection services,
        RabbitMqSettings rabbitMqSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var repository = serviceProvider.GetRequiredService<IRepository>();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
            rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
        var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

        services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

        serviceProvider = services.BuildServiceProvider();
        var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
        consumers = consumers.Concat(new List<IConsumer>
        {
            new ReceiveBrewOrderConsumer(repository, mufloneConnectionFactory, loggerFactory),
            new BrewOrderReceivedConsumer(serviceProvider.GetRequiredService<IBrewOrderService>(),
                serviceProvider.GetRequiredService<IEventBus>(), mufloneConnectionFactory, loggerFactory),
            new CloseBrewOrderConsumer(repository, mufloneConnectionFactory, loggerFactory),
            new BrewOrderClosedConsumer(serviceProvider.GetRequiredService<IBrewOrderService>(),
                serviceProvider.GetRequiredService<IEventBus>(), mufloneConnectionFactory, loggerFactory)
        });
        services.AddMufloneRabbitMQConsumers(consumers);

        return services;
    }
}