using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpLogistics.Messages.Events;

public sealed class BrewOrderSent(BrewOrderId aggregateId, Guid correlationId, BrewOrderBody brewOrderBody) : DomainEvent(aggregateId, correlationId)
{
	public readonly BrewOrderId BrewOrderId = aggregateId;
	public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}