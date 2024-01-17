using BrewApp.Modules.Orders.Extensions.Contracts;
using BrewApp.Shared.Configuration;

namespace BrewApp.Modules.Orders.Extensions.Services;

public interface IBrewOrderService
{
	Task SendBrewOrderAsync(BrewOrderJson brewOrder);

	Task<SignalRConnectionInfo> GetSignalRConnectionInfoAsync();
}