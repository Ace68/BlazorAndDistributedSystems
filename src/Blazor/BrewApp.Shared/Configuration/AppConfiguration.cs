namespace BrewApp.Shared.Configuration;

public class AppConfiguration
{
    public string Platform { get; set; } = string.Empty;
    public string BrewOrderApiUri { get; set; } = string.Empty;
    public string SignalRUri { get; set; } = string.Empty;
}