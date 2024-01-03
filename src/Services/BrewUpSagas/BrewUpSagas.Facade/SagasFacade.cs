using BrewUpSagas.Facade.Converters;
using BrewUpSagas.Shared.BindingContracts;
using Muflone.Persistence;

namespace BrewUpSagas.Facade;

public sealed class SagasFacade(IServiceBus serviceBus) : ISagasFacade
{
    public async Task SendBrewOrderAsync(BrewOrderContract body, CancellationToken cancellationToken)
    {
        await serviceBus.SendAsync(body.ToCommand(), cancellationToken);
    }
}