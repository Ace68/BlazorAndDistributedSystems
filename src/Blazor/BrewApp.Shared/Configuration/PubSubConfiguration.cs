namespace BrewApp.Shared.Configuration;

public record PubSubConfiguration(string ConnectionString, string HubName, string ClientUrl);