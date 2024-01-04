using BrewUpLogistics.Domain.Aggregates;
using BrewUpLogistics.Messages.Commands;
using BrewUpLogistics.Shared.DomainIds;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpLogistics.Domain.CommandHandlers;

public sealed class SendBrewOrderCommandHandler : CommandHandlerBaseAsync<SendBrewOrder>
{
	public SendBrewOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task ProcessCommand(SendBrewOrder command, CancellationToken cancellationToken = default)
	{
		var aggregate = Warehouse.CreateWarehouse(new WarehouseId(command.BrewOrderId.Value));
		aggregate.SentBrewOrder(command.BrewOrderId, command.MessageId, command.BrewOrderBody);
		await Repository.SaveAsync(aggregate, Guid.NewGuid());
	}
}