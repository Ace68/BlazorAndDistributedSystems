using Muflone.Core;

namespace BrewUpSagas.Shared.DomainIds;

public sealed class BrewOrderId : DomainId
{
    public BrewOrderId(Guid value) : base(value)
    {
    }
}