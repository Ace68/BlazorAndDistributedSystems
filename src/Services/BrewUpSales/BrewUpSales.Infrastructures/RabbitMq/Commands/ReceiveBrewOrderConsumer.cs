using BrewUpSales.Domain.CommandHandlers;
using BrewUpSales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSales.Infrastructures.RabbitMq.Commands;

public sealed class ReceiveBrewOrderConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : CommandConsumerBase<ReceiveBrewOrder>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<ReceiveBrewOrder> HandlerAsync { get; } = new ReceiveBrewOrderCommandHandlerAsync(repository, loggerFactory);
}