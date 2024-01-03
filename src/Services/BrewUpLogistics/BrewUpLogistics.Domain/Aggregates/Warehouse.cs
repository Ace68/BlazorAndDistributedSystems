using BrewUpLogistics.Messages.Events;
using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Core;

namespace BrewUpLogistics.Domain.Aggregates;

public class Warehouse : AggregateRoot
{
    private WarehouseId _warehouseId;

    protected Warehouse()
    {
    }

    #region Create
    internal static Warehouse CreateWarehouse(WarehouseId warehouseId)
    {
        return new(warehouseId);
    }

    private Warehouse(WarehouseId warehouseId)
    {
        Id = warehouseId;
    }
    #endregion

    #region SentBrewOrder
    internal void SentBrewOrder(BrewOrderId brewOrderId, Guid correlationId, BrewOrderBody brewOrderBody)
    {
        RaiseEvent(new BrewOrderSent(brewOrderId, correlationId, brewOrderBody));
    }

    private void Apply(BrewOrderSent @event)
    {
    }
    #endregion
}