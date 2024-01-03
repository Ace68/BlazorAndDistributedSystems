using BrewUpSagas.Shared.BindingContracts;

namespace BrewUpSagas.Facade;

public interface ISagasFacade
{
    Task SendBrewOrderAsync(BrewOrderContract body, CancellationToken cancellationToken);
}