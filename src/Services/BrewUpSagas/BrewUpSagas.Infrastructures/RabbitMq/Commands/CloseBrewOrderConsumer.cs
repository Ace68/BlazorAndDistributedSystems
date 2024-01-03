using BrewUpSagas.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUpSagas.Infrastructures.RabbitMq.Commands;

public sealed class CloseBrewOrderConsumer : CommandSenderBase<CloseBrewOrder>
{
    public CloseBrewOrderConsumer(IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
        : base(connectionFactory, loggerFactory)
    {
    }
}