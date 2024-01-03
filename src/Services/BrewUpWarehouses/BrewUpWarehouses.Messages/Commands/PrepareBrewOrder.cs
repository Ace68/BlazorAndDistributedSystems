using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpWarehouses.Messages.Commands;

public sealed class PrepareBrewOrder(BrewOrderId aggregateId, Guid commitId, BrewOrderBody brewOrderBody)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}