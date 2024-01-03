using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;
using Muflone.Messages.Events;

namespace BrewUpSales.Messages.DomainEvents;

public sealed class BrewOrderReceived(BrewOrderId aggregateId, Guid commitId, BrewOrderNumber xmasLetterNumber,
        ReceivedOn receivedOn, BrewOrderBody letterBody, BrewOrderStatus xmasLetterStatus)
    : DomainEvent(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderNumber BrewOrderNumber = xmasLetterNumber;

    public readonly ReceivedOn ReceivedOn = receivedOn;
    public readonly BrewOrderBody BrewOrderBody = letterBody;

    public readonly BrewOrderStatus BrewOrderStatus = xmasLetterStatus;
}