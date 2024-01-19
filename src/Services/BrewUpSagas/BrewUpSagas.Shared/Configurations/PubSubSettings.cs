namespace BrewUpSagas.Shared.Configurations;

public class PubSubSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string HubName { get; set; } = string.Empty;
    public string ClientUrl { get; set; } = string.Empty;
}