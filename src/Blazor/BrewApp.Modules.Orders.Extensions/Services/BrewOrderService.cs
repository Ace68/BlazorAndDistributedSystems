using BrewApp.Modules.Orders.Extensions.Contracts;
using BrewApp.Shared.Abstracts;
using BrewApp.Shared.Configuration;

namespace BrewApp.Modules.Orders.Extensions.Services;

public sealed class BrewOrderService(IHttpService httpService,
		AppConfiguration appConfiguration) : IBrewOrderService
{
	public async Task SendBrewOrderAsync(BrewOrderJson brewOrder)
	{
		await httpService.Post($"{appConfiguration.BrewOrderApiUri}v1/sagas/breworders", brewOrder);
	}

	public async Task<SignalRConnectionInfo> GetSignalRConnectionInfoAsync()
	{
		return await httpService.Post<SignalRConnectionInfo>($"{appConfiguration.SignalRUri}");
	}
}