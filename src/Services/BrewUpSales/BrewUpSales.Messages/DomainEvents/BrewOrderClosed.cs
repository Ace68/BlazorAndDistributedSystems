using BrewUpSales.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpSales.Messages.DomainEvents;

public sealed class BrewOrderClosed(BrewOrderId aggregateId, Guid commitId)
    : DomainEvent(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
}