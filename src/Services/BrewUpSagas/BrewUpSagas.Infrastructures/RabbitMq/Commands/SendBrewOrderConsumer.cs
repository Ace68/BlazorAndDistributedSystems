using BrewUpSagas.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSagas.Infrastructures.RabbitMq.Commands;

public sealed class SendBrewOrderConsumer : CommandSenderBase<SendBrewOrder>
{
    public SendBrewOrderConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) : base(
        connectionFactory, loggerFactory)
    {
    }
}