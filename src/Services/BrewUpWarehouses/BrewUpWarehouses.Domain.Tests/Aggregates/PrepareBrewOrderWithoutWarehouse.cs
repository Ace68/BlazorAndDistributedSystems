using BrewUpWarehouses.Domain.CommandHandlers;
using BrewUpWarehouses.Messages.Commands;
using BrewUpWarehouses.Messages.Events;
using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUpWarehouses.Domain.Tests.Aggregates;

public class PrepareBrewOrderWithoutWarehouse : CommandSpecification<PrepareBrewOrder>
{
    private readonly BrewOrderId _brewOrderId;
    private readonly Guid _correlationId = Guid.NewGuid();
    private readonly BrewOrderBody _brewOrderBody = new("I wish a beer");

    public PrepareBrewOrderWithoutWarehouse()
    {
        var domainId = Guid.NewGuid();
        _brewOrderId = new BrewOrderId(domainId);
    }

    protected override IEnumerable<DomainEvent> Given()
    {
        yield break;
    }

    protected override PrepareBrewOrder When()
    {
        return new PrepareBrewOrder(_brewOrderId, _correlationId, _brewOrderBody);
    }

    protected override ICommandHandlerAsync<PrepareBrewOrder> OnHandler()
    {
        return new PrepareBrewOrderCommandHandler(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BrewOrderPrepared(_brewOrderId, _correlationId, _brewOrderBody);
    }
}