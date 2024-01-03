using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.Shared.BindingContracts;
using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;

namespace BrewUpSales.ReadModel.Entities;

public class BrewOrder : EntityBase
{
    public string BrewOrderNumber { get; private set; } = string.Empty;
    public DateTime ReceivedOn { get; private set; } = DateTime.MinValue;

    public string BrewOrderBody { get; private set; } = string.Empty;
    public string BrewOrderStatus { get; private set; } = string.Empty;

    protected BrewOrder()
    {
    }

    internal static BrewOrder CreateBrewOrder(BrewOrderId aggregateId, BrewOrderNumber brewOrderNumber, ReceivedOn receivedOn,
        BrewOrderBody brewOrderBody, BrewOrderStatus brewOrderStatus) =>
        new(aggregateId.Value.ToString(), brewOrderNumber.Value, receivedOn.Value, brewOrderBody.Value, brewOrderStatus.Name);

    private BrewOrder(string bewOrderId, string brewOrderNumber, DateTime receivedOn, string orderBody, string orderStatus)
    {
        Id = bewOrderId;
        BrewOrderNumber = brewOrderNumber;

        ReceivedOn = receivedOn;

        BrewOrderBody = orderBody;
        BrewOrderStatus = orderStatus;
    }

    internal void CloseBrewOrder() => BrewOrderStatus = Shared.Enums.BrewOrderStatus.Processed.Name;

    public BrewOrderContract ToJson() => new()
    {
        BrewOrderNumber = BrewOrderNumber,
        ReceivedOn = ReceivedOn,

        OrderBody = BrewOrderBody,
        OrderStatus = BrewOrderStatus
    };
}