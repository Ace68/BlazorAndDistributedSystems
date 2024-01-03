using BrewUpSagas.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSagas.Infrastructures.RabbitMq.Commands;

public class ReceiveBrewOrderConsumer : CommandSenderBase<ReceiveBrewOrder>
{
    public ReceiveBrewOrderConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory) : base(
        connectionFactory, loggerFactory)
    {
    }
}