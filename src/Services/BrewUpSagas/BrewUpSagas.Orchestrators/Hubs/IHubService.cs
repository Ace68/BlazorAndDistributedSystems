using Microsoft.AspNetCore.SignalR;

namespace BrewUpSagas.Orchestrators.Hubs;

public interface IHubService
{
	void RegisterHubContext(IHubContext<BrewUpHub, IHubsHelper> hubContext);

	Task TellEveryoneThatClientIsConnected(string user, string message);
	Task TellEveryoneThatClientIsDisconnected(string user, string message);

	Task TellEveryoneThatBrewOrderSagaWasStarted(string user, string message);
	Task TellEveryoneThatBrewOrderWasApproved(string user, string message);
	Task TellEveryoneThatBrewOrderWasProcessed(string user, string message);
	Task TellEveryoneThatBrewOrderSagaWasCompleted(string user, string message);
}