using BrewApp.Modules.Orders.Extensions.Contracts;
using BrewApp.Modules.Orders.Extensions.Services;
using BrewApp.Shared.Messages;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using Websocket.Client;

namespace BrewApp.Modules.Orders;

public class OrdersBase : ComponentBase, IDisposable
{
	[Inject] private IBrewOrderService BrewOrderService { get; set; } = default!;

	protected string PubSubStatus { get; set; } = "Brewer Connecting ...";
	protected string TellEveryoneThatClientIsConnected { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderWasApproved { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderWasProcessed { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderSagaWasCompleted { get; set; } = string.Empty;

	protected bool HideWaitingForNewOrder { get; set; } = true;
	protected bool HideOrderAccepted { get; set; } = true;
	protected bool HideOrderProcessed { get; set; } = true;
	protected bool HideSagaCompleted { get; set; } = true;

	private WebsocketClient _webSocketClient = default!;

	protected async Task StartPubSubAsync()
	{
		var pubSubConfiguration = await BrewOrderService.GetWebPubSubConnectionStringAsync();

		PubSubStatus = pubSubConfiguration.ClientUrl;

		try
		{
			_webSocketClient = new WebsocketClient(new Uri(pubSubConfiguration.ClientUrl));

			_webSocketClient.MessageReceived.Subscribe(async (message) => await HandlePubSubMessageAsync(message));

			await _webSocketClient.Start();

			PubSubStatus = _webSocketClient.IsStarted ? "Brewer is Connected" : "Brewer Is Not Connected";
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	protected async Task StopPubSubAsync()
	{
		HideWaitingForNewOrder = true;
		HideOrderAccepted = true;
		HideOrderProcessed = true;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	protected async Task OrderBeerAsync()
	{
		BrewOrderJson brewOrder = new()
		{
			BrewOrderNumber = $"{DateTime.UtcNow.Year:0000}{DateTime.UtcNow.Month:00}{DateTime.UtcNow.Day:00}-{DateTime.UtcNow.Hour:00}{DateTime.UtcNow.Minute:00}",
			ReceivedOn = DateTime.UtcNow,
			BrewOrderBody = "I wish a Beer"
		};
		await BrewOrderService.SendBrewOrderAsync(brewOrder);
	}

	private async Task HandlePubSubMessageAsync(ResponseMessage message)
	{
		var pubSubMessage = JsonSerializer.Deserialize<PubSubMessage>(message.Text!);

		switch (pubSubMessage!.MessageType)
		{
			case "TellEveryoneThatClientIsConnected":
				await UpdateTellEveryoneThatClientIsConnectedAsync(pubSubMessage.Message).ConfigureAwait(false);
				break;

			case "TellEveryoneThatBrewOrderWasApproved":
				await UpdateTellEveryoneThatBrewOrderWasApprovedAsync(pubSubMessage.Message).ConfigureAwait(false);
				break;

			case "TellEveryoneThatBrewOrderWasProcessed":
				await UpdateTellEveryoneThatBrewOrderWasProcessedAsync(pubSubMessage.Message).ConfigureAwait(false);
				break;

			case "TellEveryoneThatBrewOrderSagaWasCompleted":
				await UpdateTellEveryoneThatBrewOrderSagaWasCompletedAsync(pubSubMessage.Message).ConfigureAwait(false);
				break;
		}

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatClientIsConnectedAsync(string message)
	{
		TellEveryoneThatClientIsConnected = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = true;
		HideOrderProcessed = true;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderWasApprovedAsync(string message)
	{
		TellEveryoneThatBrewOrderWasApproved = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = false;
		HideOrderProcessed = true;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderWasProcessedAsync(string message)
	{
		TellEveryoneThatBrewOrderWasProcessed = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = false;
		HideOrderProcessed = false;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderSagaWasCompletedAsync(string message)
	{
		TellEveryoneThatBrewOrderSagaWasCompleted = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = false;
		HideOrderProcessed = false;
		HideSagaCompleted = false;

		await InvokeAsync(StateHasChanged);
	}

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			_webSocketClient.Dispose();
		}
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~OrdersBase()
	{
		Dispose(false);
	}
	#endregion
}