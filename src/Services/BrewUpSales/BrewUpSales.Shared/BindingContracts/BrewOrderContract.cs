namespace BrewUpSales.Shared.BindingContracts;

public class BrewOrderContract
{
    public string BrewOrderNumber { get; set; } = string.Empty;

    public DateTime ReceivedOn { get; set; } = DateTime.MinValue;
    public string OrderBody { get; set; } = string.Empty;

    public string OrderStatus { get; set; } = string.Empty;
}