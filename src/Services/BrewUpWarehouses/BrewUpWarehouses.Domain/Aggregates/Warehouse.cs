using BrewUpWarehouses.Messages.Events;
using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;
using Muflone.Core;

namespace BrewUpWarehouses.Domain.Aggregates;

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

    #region PrepareXmasPresents

    internal void PrepareXmasPresents(BrewOrderId xmasLetterId, Guid correlationId, BrewOrderBody letterBody)
    {
        RaiseEvent(new BrewOrderPrepared(xmasLetterId, correlationId, letterBody));
    }

    private void Apply(BrewOrderPrepared @event)
    {
    }
    #endregion
}