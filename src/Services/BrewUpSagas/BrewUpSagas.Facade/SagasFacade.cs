using BrewUpSagas.Facade.Converters;
using BrewUpSagas.Orchestrators.Hubs;
using BrewUpSagas.Shared.BindingContracts;
using BrewUpSagas.Shared.Configurations;
using Microsoft.Extensions.Options;
using Muflone.Persistence;

namespace BrewUpSagas.Facade;

public sealed class SagasFacade(IServiceBus serviceBus, IOptions<PubSubSettings> options, IHubService hubService) : ISagasFacade
{
	public async Task SendBrewOrderAsync(BrewOrderContract body, CancellationToken cancellationToken)
	{
		await serviceBus.SendAsync(body.ToCommand(), cancellationToken);
	}

	public Task<PubSubSettings> GetPubSubSettingsAsync(CancellationToken cancellationToken)
	{
		PubSubSettings pubSubSettings = new()
		{
			ConnectionString = string.Empty,
			HubName = options.Value.HubName,
			ClientUrl = hubService.GetClientUrl()
		};
		return Task.FromResult(pubSubSettings);
	}
}