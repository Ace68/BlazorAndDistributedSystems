using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;
using Muflone.Core;

namespace BrewUpSales.Domain.Aggregates;

public class BrewOrder : AggregateRoot
{
	private BrewOrderId _brewOrderId;
	private BrewOrderNumber _brewOrderNumber;

	private ReceivedOn _receivedOn;
	private BrewOrderBody _brewOrderBody;

	private BrewOrderStatus _brewOrderStatus;

	protected BrewOrder()
	{
	}

	internal static BrewOrder ReceiveBrewOrder(BrewOrderId brewOrderId, Guid correlationId,
		BrewOrderNumber brewOrderNumber, ReceivedOn receivedOn, BrewOrderBody brewOrderBody)
	{
		var brewOrder = new BrewOrder(brewOrderId, correlationId, brewOrderNumber, receivedOn, brewOrderBody);
		return brewOrder;
	}

	private BrewOrder(BrewOrderId brewOrderId, Guid correlationId, BrewOrderNumber brewOrderNumber,
		ReceivedOn receivedOn, BrewOrderBody brewOrderBody)
	{
		RaiseEvent(new BrewOrderReceived(brewOrderId, correlationId, brewOrderNumber, receivedOn, brewOrderBody,
			BrewOrderStatus.Received));
	}

	private void Apply(BrewOrderReceived @event)
	{
		Id = @event.BrewOrderId;

		_brewOrderId = @event.BrewOrderId;
		_brewOrderNumber = @event.BrewOrderNumber;

		_receivedOn = @event.ReceivedOn;
		_brewOrderBody = @event.BrewOrderBody;
	}

	internal void CloseBrewOrder(Guid correlationId)
	{
		RaiseEvent(new BrewOrderClosed(_brewOrderId, correlationId));
	}

	private void Apply(BrewOrderClosed @event)
	{
		_brewOrderStatus = BrewOrderStatus.Processed;
	}
}