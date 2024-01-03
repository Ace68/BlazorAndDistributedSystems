using BrewUpSagas.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpSagas.Messages.IntegrationEvents;

public sealed class BrewOrderProcessed
    (BrewOrderId aggregateId, Guid correlationId) : IntegrationEvent(aggregateId, correlationId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly Guid CorrelationId = correlationId;
}