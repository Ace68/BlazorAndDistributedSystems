using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;

namespace BrewUpSagas.Orchestrators.Hubs;

public sealed class HubService(IHubContext<BrewUpHub, IHubsHelper> hubContext) : BackgroundService, IHubService
{
	public ObservableCollection<OutMessage> MessagesOutbox { get; set; } = new();

	public void Publish(string user, string message, string method)
	{
		MessagesOutbox.Add(new OutMessage(user, message, "TellEveryoneThatClientIsConnected"));
	}

	public async Task TellEveryoneThatClientIsConnected(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatClientIsConnected(user, message).ConfigureAwait(false);
	}

	public async Task TellEveryoneThatClientIsDisconnected(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatClientIsDisconnected(user, message).ConfigureAwait(false);
	}

	public async Task TellEveryoneThatBrewOrderSagaWasStarted(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatBrewOrderSagaWasStarted(user, message).ConfigureAwait(false);
	}

	public async Task TellEveryoneThatBrewOrderWasApproved(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatBrewOrderWasApproved(user, message).ConfigureAwait(false);
	}

	public async Task TellEveryoneThatBrewOrderWasProcessed(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatBrewOrderWasProcessed(user, message).ConfigureAwait(false);
	}

	public async Task TellEveryoneThatBrewOrderSagaWasCompleted(string user, string message)
	{
		await hubContext.Clients.All.TellEveryoneThatBrewOrderSagaWasCompleted(user, message).ConfigureAwait(false);
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{

			await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
		}
	}
}