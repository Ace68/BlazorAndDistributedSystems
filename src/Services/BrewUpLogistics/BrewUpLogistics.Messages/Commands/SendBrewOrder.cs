using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpLogistics.Messages.Commands;

public sealed class SendBrewOrder(BrewOrderId aggregateId, Guid commitId, BrewOrderBody letterBody)
    : Command(aggregateId, commitId)
{
    public readonly BrewOrderId XmasLetterId = aggregateId;
    public readonly BrewOrderBody LetterBody = letterBody;
}