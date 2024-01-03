using BrewUpSagas.Shared.CustomTypes;
using BrewUpSagas.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpSagas.Messages.IntegrationEvents;

public sealed class BrewOrderApproved
    (BrewOrderId aggregateId, Guid correlationId, BrewOrderBody brewOrderBody) : IntegrationEvent(aggregateId, correlationId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}