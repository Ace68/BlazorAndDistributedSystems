using Microsoft.AspNetCore.SignalR;

namespace BrewUpSagas.Orchestrators.Hubs;

public sealed class HubService : IHubService
{
    public static IHubContext<XmasHub, IHubsHelper>? HubContext;

    public void RegisterHubContext(IHubContext<XmasHub, IHubsHelper> hubContext)
    {
        HubContext = hubContext;
    }

    public async Task TellEveryoneThatClientIsConnected(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatClientIsConnected(user, message).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatClientIsDisconnected(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatClientIsDisconnected(user, message).ConfigureAwait(false);
    }

    public async Task TelEveryoneThatBrewOrderSagaWasStarted(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatBrewOrderWasStarted(user, message).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderWasApproved(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatBrewOrderWasApproved(user, message).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderWasProcessed(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatBrewOrderWasProcessed(user, message).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderSagaWasCompleted(string user, string message)
    {
        await HubContext!.Clients.All.TellEveryoneThatBrewOrderWasCompleted(user, message).ConfigureAwait(false);
    }
}