using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpLogistics.Messages.Events;

public sealed class BrewOrderProcessed
	(BrewOrderId aggregateId, Guid correlationId) : IntegrationEvent(aggregateId, correlationId)
{
	public readonly BrewOrderId BrewOrderId = aggregateId;
	public readonly Guid CorrelationId = correlationId;
}