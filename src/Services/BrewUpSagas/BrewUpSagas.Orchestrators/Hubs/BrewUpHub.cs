using Microsoft.AspNetCore.SignalR;

namespace BrewUpSagas.Orchestrators.Hubs;

public class BrewUpHub : Hub<IHubsHelper>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.TellEveryoneThatClientIsConnected("Brewer", "Brewer is Connected").ConfigureAwait(false);
        await Clients.All.TellEveryoneThatClientIsConnected("Brewer", "Waiting for beers").ConfigureAwait(false);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Clients.All.TellEveryoneThatClientIsDisconnected("Brewer", "Brewer is Disconnected").ConfigureAwait(false);

        await base.OnDisconnectedAsync(exception);
    }
}