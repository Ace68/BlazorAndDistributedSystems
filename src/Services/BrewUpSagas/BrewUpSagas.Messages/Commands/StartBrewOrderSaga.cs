using BrewUpSagas.Shared.CustomTypes;
using BrewUpSagas.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpSagas.Messages.Commands;

public class StartBrewOrderSaga(BrewOrderId aggregateId, Guid commitId, BrewOrderNumber brewOrderNumber,
        ReceivedOn receivedOn, BrewOrderBody brewOrderBody)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId BrewOrderId = aggregateId;
    public readonly BrewOrderNumber BrewOrderNumber = brewOrderNumber;

    public readonly ReceivedOn ReceivedOn = receivedOn;
    public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}