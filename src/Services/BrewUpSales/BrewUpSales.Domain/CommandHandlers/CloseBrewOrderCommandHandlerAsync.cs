using BrewUpSales.Domain.Aggregates;
using BrewUpSales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpSales.Domain.CommandHandlers;

public sealed class CloseBrewOrderCommandHandlerAsync : CommandHandlerBaseAsync<CloseBrewOrder>
{
    public CloseBrewOrderCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(CloseBrewOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = await Repository.GetByIdAsync<BrewOrder>(command.AggregateId.Value);
        aggregate.CloseXmasLetter(command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}