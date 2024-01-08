using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace BrewUpSagas.Orchestrators.Hubs;

public sealed class HubService(IHubContext<BrewUpHub, IHubsHelper> hubContext) : BackgroundService, IHubService
{
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

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		SagaBroker.MessagesOutbox.CollectionChanged += OnMessageReceived;

		return Task.CompletedTask;
	}

	private void OnMessageReceived(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
	{
		if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
		{
			foreach (OutMessage message in args.NewItems)
			{
				switch (message.Method)
				{
					case "TellEveryoneThatClientIsConnected":
						Task.Run(async () => await TellEveryoneThatClientIsConnected(message.User, message.Message));
						break;

					case "TellEveryoneThatBrewOrderSagaWasStarted":
						Task.Run(async () => await TellEveryoneThatBrewOrderSagaWasStarted(message.User, message.Message));
						break;

					case "TellEveryoneThatBrewOrderWasApproved":
						Task.Run(async () => await TellEveryoneThatBrewOrderWasApproved(message.User, message.Message));
						break;

					case "TellEveryoneThatBrewOrderWasProcessed":
						Task.Run(async () => await TellEveryoneThatBrewOrderWasProcessed(message.User, message.Message));
						break;

					case "TellEveryoneThatBrewOrderSagaWasCompleted":
						Task.Run(async () => await TellEveryoneThatBrewOrderSagaWasCompleted(message.User, message.Message));
						break;
				}

			}
		}
	}
}