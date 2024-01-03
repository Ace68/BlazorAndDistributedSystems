using BrewUpSales.Domain.CommandHandlers;
using BrewUpSales.Messages.Commands;
using BrewUpSales.Messages.DomainEvents;
using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUpSales.Domain.Tests.Aggregates;

public sealed class CloseBrewOrderSuccessfully : CommandSpecification<CloseBrewOrder>
{
    private readonly BrewOrderId _brewOrderId = new(Guid.NewGuid());

    private readonly BrewOrderNumber _brewOrderNumber =
        new(
            $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}");

    private readonly Guid _commitId = Guid.NewGuid();

    private readonly ReceivedOn _receivedOn = new(DateTime.UtcNow);
    private readonly BrewOrderBody _brewOrderBody = new("I want a beer");

    protected override IEnumerable<DomainEvent> Given()
    {
        yield return new BrewOrderReceived(_brewOrderId, _commitId, _brewOrderNumber, _receivedOn, _brewOrderBody, BrewOrderStatus.Received);
    }

    protected override CloseBrewOrder When()
    {
        return new CloseBrewOrder(_brewOrderId, _commitId);
    }

    protected override ICommandHandlerAsync<CloseBrewOrder> OnHandler()
    {
        return new CloseBrewOrderCommandHandlerAsync(Repository, new NullLoggerFactory());
    }

    protected override IEnumerable<DomainEvent> Expect()
    {
        yield return new BrewOrderClosed(_brewOrderId, _commitId);
    }
}