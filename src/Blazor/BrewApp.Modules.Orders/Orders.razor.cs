using BrewApp.Shared.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BrewApp.Modules.Orders;

public class OrdersBase : ComponentBase, IDisposable
{
	[Inject] private AppConfiguration AppConfiguration { get; set; } = default!;

	protected string SignalRStatus { get; set; } = "Connecting ...";

	private HubConnection? _hubConnection;

	protected override async Task OnInitializedAsync()
	{
		_hubConnection = new HubConnectionBuilder()
			.WithUrl(new Uri(AppConfiguration.SignalRUri))
			.WithServerTimeout(TimeSpan.FromSeconds(60))
			.WithKeepAliveInterval(TimeSpan.FromSeconds(15))
			.WithAutomaticReconnect()
			.Build();

		_hubConnection.On<string, string>("TellEveryoneThatClientIsConnected", UpdateBrewOrderMessagesAsync);

		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderSagaWasStarted", UpdateBrewOrderMessagesAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderWasApproved", UpdateBrewOrderMessagesAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderWasProcessed", UpdateBrewOrderMessagesAsync);
		_hubConnection.On<string, string>("TellEveryoneThatBrewOrderSagaWasCompleted", UpdateBrewOrderMessagesAsync);

		await _hubConnection.StartAsync();

		SignalRStatus = _hubConnection.State == HubConnectionState.Connected ? "SignalR is Connected" : "SignalR Is Not Connected";

		await base.OnInitializedAsync();
	}

	private async Task UpdateBrewOrderMessagesAsync(string target, string message)
	{
		if (string.IsNullOrWhiteSpace(message))
			message = "No Message";

		//XmasLetterMessages = XmasLetterMessages.Concat(new List<string>
		//{
		//	message
		//});

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