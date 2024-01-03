using BrewUpLogistics.Domain.CommandHandlers;
using BrewUpLogistics.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpLogistics.Infrastructures.RabbitMq.Commands;

public sealed class SendBrewOrderConsumer(IRepository repository, IMufloneConnectionFactory connectionFactory,
        ILoggerFactory loggerFactory)
    : CommandConsumerBase<SendBrewOrder>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<SendBrewOrder> HandlerAsync { get; } = new SendBrewOrderCommandHandler(repository, loggerFactory);
}