namespace EspHomeTools.sections;

public record EspHomeSection
{
    public string Name { get; init; } = "default-node";
    public string FriendlyName { get; init; } = string.Empty;
}