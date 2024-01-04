using BrewUpLogistics.Shared.CustomTypes;
using BrewUpLogistics.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUpLogistics.Messages.Commands;

public sealed class SendBrewOrder(BrewOrderId aggregateId, Guid commitId, BrewOrderBody brewOrderBody)
	: Command(aggregateId, commitId)
{
	public readonly BrewOrderId BrewOrderId = aggregateId;
	public readonly BrewOrderBody BrewOrderBody = brewOrderBody;
}