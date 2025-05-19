using BrewUpSales.Domain.Aggregates;
using BrewUpSales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpSales.Domain.CommandHandlers;

public sealed class ReceiveBrewOrderCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<ReceiveBrewOrder>(repository, loggerFactory)
{
    public override async Task ProcessCommand(ReceiveBrewOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = BrewOrder.ReceiveBrewOrder(command.BrewOrderId, command.MessageId, command.BrewOrderNumber,
            command.ReceivedOn, command.BrewOrderBody);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}