using BrewUpSales.Domain.Aggregates;
using BrewUpSales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpSales.Domain.CommandHandlers;

public sealed class ReceiveBrewOrderCommandHandlerAsync : CommandHandlerBaseAsync<ReceiveBrewOrder>
{
    public ReceiveBrewOrderCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(ReceiveBrewOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = BrewOrder.ReceiveBrewOrder(command.BrewOrderId, command.MessageId, command.BrewOrderNumber,
            command.ReceivedOn, command.BrewOrderBody);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}