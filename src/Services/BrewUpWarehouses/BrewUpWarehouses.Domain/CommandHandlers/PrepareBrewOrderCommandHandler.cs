using BrewUpWarehouses.Domain.Aggregates;
using BrewUpWarehouses.Messages.Commands;
using BrewUpWarehouses.Shared.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpWarehouses.Domain.CommandHandlers;

public sealed class PrepareBrewOrderCommandHandler : CommandHandlerBaseAsync<PrepareBrewOrder>
{
    public PrepareBrewOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task ProcessCommand(PrepareBrewOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = Warehouse.CreateWarehouse(new WarehouseId(command.BrewOrderId.Value));
        aggregate.PrepareXmasPresents(command.BrewOrderId, command.MessageId, command.BrewOrderBody);
        await Repository.SaveAsync(aggregate, Guid.NewGuid());
    }
}