namespace BrewUpSagas.Orchestrators.Hubs;

public interface IHubsHelper
{
    Task TellEveryoneThatClientIsConnected(string user, string message);
    Task TellEveryoneThatClientIsDisconnected(string user, string message);

    Task TellEveryoneThatBrewOrderWasStarted(string user, string message);
    Task TellEveryoneThatBrewOrderWasApproved(string user, string message);
    Task TellEveryoneThatBrewOrderWasProcessed(string user, string message);
    Task TellEveryoneThatBrewOrderWasCompleted(string user, string message);
}