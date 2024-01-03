using BrewUpSagas.Shared.CustomTypes;
using BrewUpSagas.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpSagas.Messages.Commands;

public sealed class SendBrewOrder(BrewOrderId aggregateId, Guid commitId, BrewOrderBody brewOrderBody)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}