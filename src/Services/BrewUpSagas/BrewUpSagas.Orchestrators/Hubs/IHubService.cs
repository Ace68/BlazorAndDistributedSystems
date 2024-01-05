namespace BrewUpSagas.Orchestrators.Hubs;

public interface IHubService
{
	Task TellEveryoneThatClientIsConnected(string user, string message);
	Task TellEveryoneThatClientIsDisconnected(string user, string message);

	Task TellEveryoneThatBrewOrderSagaWasStarted(string user, string message);
	Task TellEveryoneThatBrewOrderWasApproved(string user, string message);
	Task TellEveryoneThatBrewOrderWasProcessed(string user, string message);
	Task TellEveryoneThatBrewOrderSagaWasCompleted(string user, string message);
}