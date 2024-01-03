using BrewUpSales.Domain.CommandHandlers;
using BrewUpSales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSales.Infrastructures.RabbitMq.Commands;

public sealed class CloseBrewOrderConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : CommandConsumerBase<CloseBrewOrder>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<CloseBrewOrder> HandlerAsync { get; } = new CloseBrewOrderCommandHandlerAsync(repository, loggerFactory);
}