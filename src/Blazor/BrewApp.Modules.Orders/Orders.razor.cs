using BrewApp.Shared.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BrewApp.Modules.Orders;

public class OrdersBase : ComponentBase, IDisposable
{
	[Inject] private AppConfiguration AppConfiguration { get; set; } = default!;

	protected string SignalRStatus { get; set; } = "Brewer Connecting ...";
	protected string TellEveryoneThatClientIsConnected { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderSagaWasStarted { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderWasProcessed { get; set; } = string.Empty;
	protected string TellEveryoneThatBrewOrderSagaWasCompleted { get; set; } = string.Empty;

	protected bool HideWaitingForNewOrder { get; set; } = true;
	protected bool HideOrderAccepted { get; set; } = true;
	protected bool HideOrderProcessed { get; set; } = true;
	protected bool HideSagaCompleted { get; set; } = true;

	private HubConnection? _hubConnection;

	protected async Task StartSignalRAsync()
	{
		_hubConnection = new HubConnectionBuilder()
			.WithUrl(new Uri(AppConfiguration.SignalRUri))
			.WithServerTimeout(TimeSpan.FromSeconds(60))
			.WithKeepAliveInterval(TimeSpan.FromSeconds(15))
			.WithAutomaticReconnect()
			.Build();

		_hubConnection.On<string, string>("TellEveryoneThatClientIsConnected", UpdateTellEveryoneThatClientIsConnectedAsync);

		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderSagaWasStarted", UpdateTellEveryoneThatBrewOrderSagaWasStartedAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderWasApproved", UpdateTellEveryoneThatBrewOrderWasProcessedAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderWasProcessed", UpdateTellEveryoneThatBrewOrderWasProcessedAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderSagaWasCompleted", UpdateTellEveryoneThatBrewOrderSagaWasCompletedAsync);

		await _hubConnection.StartAsync();

		SignalRStatus = _hubConnection.State == HubConnectionState.Connected ? "Brewer is Connected" : "Brewer Is Not Connected";
	}

	private async Task UpdateTellEveryoneThatClientIsConnectedAsync(string target, string message)
	{
		TellEveryoneThatClientIsConnected = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = true;
		HideOrderProcessed = true;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderSagaWasStartedAsync(string target, string message)
	{
		TellEveryoneThatBrewOrderSagaWasStarted = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = false;
		HideOrderProcessed = true;
		HideSagaCompleted = true;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderWasProcessedAsync(string target, string message)
	{
		TellEveryoneThatBrewOrderWasProcessed = message;

		HideWaitingForNewOrder = false;
		HideOrderAccepted = false;
		HideOrderProcessed = false;
		HideSagaCompleted = false;

		await InvokeAsync(StateHasChanged);
	}

	private async Task UpdateTellEveryoneThatBrewOrderSagaWasCompletedAsync(string target, string message)
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