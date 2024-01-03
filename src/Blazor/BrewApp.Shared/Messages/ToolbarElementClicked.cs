using BrewApp.Shared.Enums;

namespace BrewApp.Shared.Messages;

public record ToolbarElementClicked(ToolbarElement ToolbarElement, ModuleNames ModuleName);