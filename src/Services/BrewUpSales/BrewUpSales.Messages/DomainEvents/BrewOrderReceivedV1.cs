using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;
using Muflone.Messages.Events;

namespace BrewUpSales.Messages.DomainEvents;

public sealed class BrewOrderReceivedV1(BrewOrderId aggregateId, Guid commitId, BrewOrderNumber brewOrderNumber,
		ReceivedOn receivedOn, BrewOrderBody brewOrderBody, BrewOrderStatus brewOrderStatus, double sconto)
	: DomainEvent(aggregateId, commitId)
{
	public readonly BrewOrderId BrewOrderId = aggregateId;
	public readonly BrewOrderNumber BrewOrderNumber = brewOrderNumber;

	public readonly ReceivedOn ReceivedOn = receivedOn;
	public readonly BrewOrderBody BrewOrderBody = brewOrderBody;

	public readonly BrewOrderStatus BrewOrderStatus = brewOrderStatus;
	public readonly double Sconto = sconto;
}