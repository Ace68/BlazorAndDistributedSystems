using Muflone.Core;

namespace BrewUpWarehouses.Shared.DomainIds;

public sealed class BrewOrderId : DomainId
{
    public BrewOrderId(Guid value) : base(value)
    {
    }
}