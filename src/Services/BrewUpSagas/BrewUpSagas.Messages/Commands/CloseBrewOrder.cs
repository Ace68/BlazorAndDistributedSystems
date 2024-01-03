using BrewUpSagas.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpSagas.Messages.Commands;

public sealed class CloseBrewOrder(BrewOrderId aggregateId, Guid commitId)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
}