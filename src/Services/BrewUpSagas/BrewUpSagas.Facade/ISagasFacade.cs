using BrewUpSagas.Shared.BindingContracts;
using BrewUpSagas.Shared.Configurations;

namespace BrewUpSagas.Facade;

public interface ISagasFacade
{
    Task SendBrewOrderAsync(BrewOrderContract body, CancellationToken cancellationToken);
    Task<PubSubSettings> GetPubSubSettingsAsync(CancellationToken cancellationToken);
}