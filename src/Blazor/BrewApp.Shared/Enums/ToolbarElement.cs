namespace BrewApp.Shared.Enums;

public class ToolbarElement : Enumeration
{
    public static ToolbarElement SendXmasLetter = new(1, "SXL", "SendXmasLetter");

    public static IEnumerable<ToolbarElement> List() => new[] { SendXmasLetter };

    public ToolbarElement(int id, string code, string name) : base(id, code, name)
    {
    }

    public static ToolbarElement FromName(string name)
    {
        var element = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (element == null)
            throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Name))}");

        return element;
    }

    public static ToolbarElement FromCode(string code)
    {
        var element = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

        if (element == null)
            throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Code))}");

        return element;
    }

    public static ToolbarElement From(int id)
    {
        var element = List().SingleOrDefault(s => s.Id == id);

        if (element == null)
            throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Name))}");

        return element;
    }
}