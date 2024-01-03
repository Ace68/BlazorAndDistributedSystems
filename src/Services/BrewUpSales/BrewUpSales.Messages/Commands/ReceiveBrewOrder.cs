using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpSales.Messages.Commands;

public sealed class ReceiveBrewOrder(BrewOrderId aggregateId, Guid commitId, BrewOrderNumber brewOrderNumber,
        ReceivedOn receivedOn, BrewOrderBody brewOrderBody)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderNumber BrewOrderNumber = brewOrderNumber;

    public readonly ReceivedOn ReceivedOn = receivedOn;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}