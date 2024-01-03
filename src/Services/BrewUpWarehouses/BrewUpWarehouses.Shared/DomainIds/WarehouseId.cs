using Muflone.Core;

namespace BrewUpWarehouses.Shared.DomainIds;

public sealed class WarehouseId : DomainId
{
    public WarehouseId(Guid value) : base(value)
    {
    }
}