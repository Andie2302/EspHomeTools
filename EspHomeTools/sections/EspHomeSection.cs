namespace EspHomeTools.sections;

public record EspHomeSection
{
    public string Name { get; init; } = "${name}";
    public string FriendlyName { get; init; } = "${friendly_name}";
}