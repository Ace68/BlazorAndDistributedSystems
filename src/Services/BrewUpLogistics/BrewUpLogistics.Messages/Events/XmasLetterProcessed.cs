using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpLogistics.Messages.Events;

public sealed class XmasLetterProcessed
    (BrewOrderId aggregateId, Guid correlationId, BrewOrderBody brewOrderBody) : IntegrationEvent(aggregateId, correlationId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly Guid CorrelationId = correlationId;
}