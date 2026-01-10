namespace EspHomeTools;

public record EsphomeSection
{
    public string Name { get; init; } = "default-node";
    public string FriendlyName { get; init; } = string.Empty;
}