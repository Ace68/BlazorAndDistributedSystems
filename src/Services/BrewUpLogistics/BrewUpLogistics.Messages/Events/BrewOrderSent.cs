using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpLogistics.Messages.Events;

public sealed class BrewOrderSent(BrewOrderId aggregateId, Guid correlationId, BrewOrderBody letterBody) : DomainEvent(aggregateId, correlationId)
{
    public readonly BrewOrderId XmasLetterId = aggregateId;
    public readonly BrewOrderBody LetterBody = letterBody;
}