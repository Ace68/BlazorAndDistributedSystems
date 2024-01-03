namespace BrewUpSagas.Shared.BindingContracts;

public class BrewOrderContract
{
    public string BrewOrderNumber { get; set; } = string.Empty;

    public DateTime ReceivedOn { get; set; } = DateTime.MinValue;
    public string BrewOrderBody { get; set; } = string.Empty;
}