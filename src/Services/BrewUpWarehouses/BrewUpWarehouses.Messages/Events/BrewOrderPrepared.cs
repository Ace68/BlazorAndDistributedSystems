using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpWarehouses.Messages.Events;

public sealed class BrewOrderPrepared(BrewOrderId aggregateId, Guid correlationId, BrewOrderBody brewOrderBody)
    : DomainEvent(aggregateId, correlationId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}