using Muflone.Core;

namespace BrewUpSales.Shared.DomainIds;

public sealed class BrewOrderId : DomainId
{
    public BrewOrderId(Guid value) : base(value)
    {
    }
}