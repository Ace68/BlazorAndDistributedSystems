using BrewUpWarehouses.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUpWarehouses.Messages.Events;

public sealed class WarehouseCreated(WarehouseId aggregateId) : DomainEvent(aggregateId)
{
    public readonly WarehouseId WarehouseId = aggregateId;
}