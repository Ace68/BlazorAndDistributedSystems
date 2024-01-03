using Muflone.Core;

namespace BrewUpLogistics.Shared.DomainIds;

public sealed class BrewOrderId : DomainId
{
    public BrewOrderId(Guid value) : base(value)
    {
    }
}