namespace BrewUpSales.Shared.Enums;

public sealed class BrewOrderStatus : Enumeration
{
    public static BrewOrderStatus Received = new(1, "Rx", "Received");
    public static BrewOrderStatus Validated = new(2, "AX", "Validated");
    public static BrewOrderStatus Processed = new(3, "PX", "Processed");

    public static IEnumerable<BrewOrderStatus> List() => new[]
    {
        Received,
        Validated,
        Processed
    };

    public BrewOrderStatus(int id, string code, string name) : base(id, code, name)
    {
    }

    public static BrewOrderStatus FromName(string name)
    {
        var element = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (element == null)
            throw new Exception($"Possible values for BrewOrderStatus: {string.Join(",", List().Select(s => s.Name))}");

        return element;
    }

    public static BrewOrderStatus FromCode(string code)
    {
        var element = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

        if (element == null)
            throw new Exception($"Possible values for BrewOrderStatus: {string.Join(",", List().Select(s => s.Code))}");

        return element;
    }

    public static BrewOrderStatus From(int id)
    {
        var element = List().SingleOrDefault(s => s.Id == id);

        if (element == null)
            throw new Exception($"Possible values for ModuleNames: {string.Join(",", List().Select(s => s.Name))}");

        return element;
    }
}