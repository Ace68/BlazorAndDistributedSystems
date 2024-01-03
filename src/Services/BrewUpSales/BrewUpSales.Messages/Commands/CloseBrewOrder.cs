using BrewUpSales.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpSales.Messages.Commands;

public sealed class CloseBrewOrder(BrewOrderId aggregateId, Guid commitId)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
}