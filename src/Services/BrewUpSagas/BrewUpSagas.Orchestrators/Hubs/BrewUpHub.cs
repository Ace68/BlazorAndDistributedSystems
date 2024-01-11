using Microsoft.AspNetCore.SignalR;

namespace BrewUpSagas.Orchestrators.Hubs;

public class BrewUpHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("TellEveryoneThatClientIsConnected", "Brewer", "Brewer is Connected").ConfigureAwait(false);
		await Clients.All.SendAsync("TellEveryoneThatClientIsConnected", "Brewer", "Waiting for new Order").ConfigureAwait(false);

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("TellEveryoneThatClientIsDisconnected", "Brewer", "Brewer is Disconnected").ConfigureAwait(false);

		await base.OnDisconnectedAsync(exception);
	}
}