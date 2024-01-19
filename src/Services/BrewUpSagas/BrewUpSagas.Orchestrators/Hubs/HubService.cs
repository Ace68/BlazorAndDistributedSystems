using Azure.Messaging.WebPubSub;
using BrewUpSagas.Messages.PubSub;
using BrewUpSagas.Shared.Configurations;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace BrewUpSagas.Orchestrators.Hubs;

public sealed class HubService(IOptions<PubSubSettings> options) : IHubService
{
    private readonly WebPubSubServiceClient _webPubSubServiceClient = new(options.Value.ConnectionString, options.Value.HubName);

    public string GetClientUrl()
    {
        return _webPubSubServiceClient.GetClientAccessUri(DateTimeOffset.UtcNow.AddDays(1)).AbsoluteUri;
    }

    public async Task TellEveryoneThatClientIsConnected(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatClientIsConnected");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatClientIsDisconnected(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatClientIsDisconnected");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderSagaWasStarted(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatBrewOrderSagaWasStarted");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderWasApproved(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatBrewOrderWasApproved");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderWasProcessed(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatBrewOrderWasProcessed");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }

    public async Task TellEveryoneThatBrewOrderSagaWasCompleted(string user, string message)
    {
        PubSubMessage pubSubMessage = new(user, message, "TellEveryoneThatBrewOrderSagaWasCompleted");
        await _webPubSubServiceClient.SendToAllAsync(JsonSerializer.Serialize(pubSubMessage)).ConfigureAwait(false);
    }
}